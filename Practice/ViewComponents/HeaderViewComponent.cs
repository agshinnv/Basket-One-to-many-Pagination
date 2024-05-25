using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Practice.Services.Interfaces;
using Practice.ViewModels;
using Practice.ViewModels.Baskets;

namespace Practice.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly ISettingService _settingService;
        private readonly IHttpContextAccessor _accessor;

        public HeaderViewComponent(ISettingService settingService, 
                                   IHttpContextAccessor accessor)
        {
            _settingService = settingService;
            _accessor = accessor;

        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            Dictionary<string,string> settingDatas = await _settingService.GetAllAsync();

            List<BasketVM> basketProducts = new();

            if (_accessor.HttpContext.Request.Cookies["basket"] is not null)
            {
                basketProducts = JsonConvert.DeserializeObject<List<BasketVM>>(_accessor.HttpContext.Request.Cookies["basket"]);
            }

            HeaderVM response = new()
            {
                Settings = settingDatas,
                BasketCount = basketProducts.Sum(m => m.Count),
                BasketTotalPrice = basketProducts.Sum(m => m.Count * m.Price)
            };

            return await Task.FromResult(View(response));

        }
    }
}
