using Microsoft.AspNetCore.Mvc;
using Practice.Services.Interfaces;
using Practice.ViewModels.Products;

namespace Practice.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int page = 1)
        {
            
            var paginateDatas = await _productService.GetAllPaginateAsync(page);

            var mappedDatas = _productService.GetMappedDatas(paginateDatas);

            ViewBag.pageCount = await GetPageCountAsync(4);

            ViewBag.currentPage = page;

            return View(mappedDatas);
        }

        private async Task<int> GetPageCountAsync(int take)
        {
            int count = await _productService.GetCountAsync();

            return (int)Math.Ceiling((decimal)count / take);
        }

    }
}
