using System.ComponentModel.DataAnnotations;

namespace Practice.ViewModels.Sliders
{
    public class SliderCreateVM
    {
        [Required]
        public List<IFormFile> Images { get; set; }
    }
}
