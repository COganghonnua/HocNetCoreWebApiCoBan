using System.ComponentModel.DataAnnotations;

namespace ThucChienEFCore.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public double OriginalPrice { get; set; }
        public double DiscountedPrice { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public string ImageData { get; set; }

        public int? CategoryId { get; set; }
        public string ? CategoryName { get; set; }
    }
}
