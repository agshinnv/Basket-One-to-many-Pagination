using Practice.Models;

namespace Practice.ViewModels
{
    public class HomeVM
    {
        public List<Category> Categories { get; set; }
        public List<Product> Products { get; set;}
        public List<Blog> Blogs { get; set; }
        public List<Say> Says { get; set; }
        public List<InstagramSlider> InstagramSliders { get; set; }
    }
}
