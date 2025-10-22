using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models

{
    public class Category
    {
        public int Id { get; set; }
        public string? Image { get; set; }

        [Required]
        [MinLength(3)]
        public string Name { get; set; }
        public List<Product> Products { get; set; } = new List<Product>();


    }
}
