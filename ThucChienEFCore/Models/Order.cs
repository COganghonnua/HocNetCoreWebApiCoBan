using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ThucChienEFCore.Models
{
    public class Order
    {
        public int Id { get; set; }

        public string CustomerName { get; set; }
        public string ShippingAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Notes { get; set; }


        public int? DiscountId { get; set; }
        public Discount DiscountCode { get; set; }

        public double DiscountAmount { get; set; }
        public double TotalAmount { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

        public string Status { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime OrderDate { get; set; }
    }
}
