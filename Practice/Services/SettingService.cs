using Microsoft.EntityFrameworkCore;
using Practice.Data;
using Practice.Services.Interfaces;

namespace Practice.Services
{
    public class SettingService : ISettingService
    {
        private readonly AppDbContext _context;

        public SettingService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Dictionary<string, string>> GetAllAsync()
        {
            var datas = await _context.Settings.ToDictionaryAsync(m => m.Key, m => m.Value);
            return datas;
        }
    }
}
