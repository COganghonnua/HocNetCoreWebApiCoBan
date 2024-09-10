using System.ComponentModel.DataAnnotations;

namespace ThucChienEFCore.Models
{
    public class Discount
    {
 
        public int Id { get; set; }

        [Required]
        public string Code { get; set; }

        public double Percentage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
