namespace Ecommerce.Models
{
    public class LastSelectedCategory
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public DateTime SelectedDate { get; set; } = DateTime.Now;
        public Category Category { get; set; }

    }
}
