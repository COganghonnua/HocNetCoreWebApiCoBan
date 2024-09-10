using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace ThucChienEFCore.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly Microsoft.AspNetCore.Hosting.IWebHostEnvironment _env;

        public ExceptionHandlingMiddleware(RequestDelegate next, Microsoft.AspNetCore.Hosting.IWebHostEnvironment env)
        {
            _next = next;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = exception switch
            {
                ArgumentNullException => StatusCodes.Status400BadRequest,
                KeyNotFoundException => StatusCodes.Status404NotFound,
                UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
                DbUpdateException => StatusCodes.Status500InternalServerError,
                ArgumentOutOfRangeException => StatusCodes.Status400BadRequest,
                InvalidOperationException => StatusCodes.Status400BadRequest,
                // Thêm các loại ngoại lệ khác nếu cần
                _ => StatusCodes.Status500InternalServerError,
            };

            var errorResponse = new
            {
                StatusCode = context.Response.StatusCode,
                Message = GetUserFriendlyMessage(exception),
                ExceptionType = exception.GetType().Name,
                // Chỉ hiển thị StackTrace trong môi trường phát triển
                StackTrace = _env.IsDevelopment() ? exception.StackTrace : null
            };

            return context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(errorResponse));
        }

        private string GetUserFriendlyMessage(Exception exception)
        {
            // Tùy chỉnh thông báo lỗi dựa trên loại ngoại lệ
            return exception switch
            {
                ArgumentNullException => "Dữ liệu đầu vào không hợp lệ.",
                KeyNotFoundException => "Không tìm thấy dữ liệu yêu cầu.",
                UnauthorizedAccessException => "Bạn không có quyền truy cập tài nguyên này.",
                DbUpdateException => "Có lỗi xảy ra khi cập nhật dữ liệu.",
                _ => "Đã xảy ra lỗi. Vui lòng thử lại sau."
            };
        }

    }
}
