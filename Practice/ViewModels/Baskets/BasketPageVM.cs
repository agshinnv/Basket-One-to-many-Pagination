using Practice.Models;

namespace Practice.ViewModels.Baskets
{
    public class BasketPageVM
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
        
    }
}
