using Practice.Models;

namespace Practice.Services.Interfaces
{
    public interface ISayService
    {
        Task<List<Say>> GetSayAsync();
    }
}
