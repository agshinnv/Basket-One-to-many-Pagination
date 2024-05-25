using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Practice.Data;
using Practice.Models;
using Practice.Services.Interfaces;
using Practice.ViewModels;
using Practice.ViewModels.Baskets;
using System.Diagnostics;
using System.Xml.Linq;

namespace Practice.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly ISayService _sayService;
        private readonly IInstagramSliderService _instagramSliderService;
        private readonly IHttpContextAccessor _accessor;
        public HomeController(AppDbContext context,
                              IProductService productService,
                              ICategoryService categoryService,
                              ISayService sayService,
                              IInstagramSliderService instagramSliderService,
                              IHttpContextAccessor accessor)
        {
            _context = context;
            _productService = productService;
            _categoryService = categoryService;
            _sayService = sayService;
            _instagramSliderService = instagramSliderService;
            _accessor = accessor;

        }
        public async Task<IActionResult> Index()
        {             
            List<Category> categories = await _categoryService.GetAllAsync();
            List<Product> products = await _productService.GetAllWithImagesAsync();
            List<Blog> blogs = await _context.Blogs.Take(3).ToListAsync();
            List<Say> says = await _sayService.GetSayAsync();
            List<InstagramSlider> instagramSliders = await _instagramSliderService.GetSliderAsync();
                                
                        

            HomeVM model = new()
            {
                Categories = categories,
                Products = products,
                Blogs = blogs,
                Says = says,
                InstagramSliders = instagramSliders

            };

            return View(model);
        }


        [HttpPost]       
        public async Task<IActionResult> AddProductToBasket(int? id)
        {
            if (id is null) return BadRequest();

            List<BasketVM> basketProducts = null;

            
            if(_accessor.HttpContext.Request.Cookies["basket"] is not null)
            {
                basketProducts = JsonConvert.DeserializeObject<List<BasketVM>>(_accessor.HttpContext.Request.Cookies["basket"]);
            }
            else
            {
                basketProducts = new List<BasketVM>();
            }


            var dbProduct = await _context.Products.FirstOrDefaultAsync(m => m.Id == (int)id);


            var existProduct = basketProducts.FirstOrDefault(m => m.Id == (int)id);

            if (existProduct is not null)
            {
                existProduct.Count++;
            }
            else
            {
                basketProducts.Add(new BasketVM
                {
                    Id = (int)id,
                    Count = 1,
                    Price = dbProduct.Price
                });
            }

            
            _accessor.HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(basketProducts));





            int count = basketProducts.Sum(m => m.Count);
            decimal total = basketProducts.Sum(m=>m.Count * m.Price);


            return Ok(new {count,total});



        }


    }
}
