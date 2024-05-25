using Microsoft.EntityFrameworkCore;
using Practice.Data;
using Practice.Models;
using Practice.Services.Interfaces;

namespace Practice.Services
{
    public class InstagramSliderService : IInstagramSliderService
    {
        private readonly AppDbContext _context;

        public InstagramSliderService(AppDbContext context)
        {
            _context = context;
        }
       

        public async Task<List<InstagramSlider>> GetSliderAsync()
        {
            return await _context.InstagramSliders.ToListAsync();
        }
    }
}
