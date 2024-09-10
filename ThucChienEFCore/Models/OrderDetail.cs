using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ThucChienEFCore.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }

        public int Quantity { get; set; }
        public double UnitPrice { get; set; }


        //Foreignkey
        public int? ProductId { get; set; }  
        public int? OrderId { get; set; }

        //Navagation
        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
