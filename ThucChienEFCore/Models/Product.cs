using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ThucChienEFCore.Models
{
    public class Product
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
        public Category ? Category { get; set; }



        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
        public ICollection<Rating> Ratings { get; set; } = new List<Rating>();
    }
}
