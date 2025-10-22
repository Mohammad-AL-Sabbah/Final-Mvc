using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        public string Description { get; set; }
        public decimal Rate { get; set; }
        public int Quantity { get; set; }
        public string Image { get; set;}
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;

    }
}
