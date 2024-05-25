using Microsoft.EntityFrameworkCore;
using Practice.Data;
using Practice.Models;
using Practice.Services.Interfaces;

namespace Practice.Services
{
    public class SayService : ISayService
    {
        private readonly AppDbContext _context;

        public SayService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Say>> GetSayAsync()
        {
            return await _context.Says.ToListAsync();
        }
    }
}
