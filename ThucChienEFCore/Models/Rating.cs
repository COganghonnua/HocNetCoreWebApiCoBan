using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ThucChienEFCore.Models
{
    public class Rating
    {
        public int Id { get; set; }

        [Required]
        [StringLength(500)]
        public string Comment { get; set; }

        public int? ProductId { get; set; }
        public Product Product { get; set; }
    }
}
