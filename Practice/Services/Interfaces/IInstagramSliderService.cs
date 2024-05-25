using Practice.Models;

namespace Practice.Services.Interfaces
{
    public interface IInstagramSliderService
    {
        Task<List<InstagramSlider>> GetSliderAsync();
    }
}
