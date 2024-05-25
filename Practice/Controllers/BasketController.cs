using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Practice.Data;
using Practice.Models;
using Practice.Services.Interfaces;
using Practice.ViewModels.Baskets;

namespace Practice.Controllers
{
    public class BasketController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _accessor;

        public BasketController(AppDbContext context,
                                IHttpContextAccessor accessor)
        {
            _context = context;
            _accessor = accessor;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<BasketVM> basketProducts = null;


            if (_accessor.HttpContext.Request.Cookies["basket"] is not null)
            {
                basketProducts = JsonConvert.DeserializeObject<List<BasketVM>>(_accessor.HttpContext.Request.Cookies["basket"]);
            }
            else
            {
                basketProducts = new List<BasketVM>();
            }


            var products = await _context.Products.Include(m=>m.Category)
                                                  .Include(m=>m.ProductImages)
                                                  .ToListAsync();


            List<BasketPageVM> basket = new();

            foreach (var item in basketProducts)
            {
                var dbProduct = products.FirstOrDefault(m=>m.Id == item.Id);

                basket.Add(new BasketPageVM
                {
                    Id = dbProduct.Id,
                    Name = dbProduct.Name,
                    Image = dbProduct.ProductImages.FirstOrDefault(m=>m.IsMain).Name,
                    Category = dbProduct.Category.Name,
                    Price = dbProduct.Price,
                    Count = item.Count

                });
            }

            CartVM response = new()
            {
                BasketProducts = basket,
                Total = basketProducts.Sum(m => m.Count * m.Price)
            };

            return View(response);
        }


        [HttpPost]
        public IActionResult DeleteProductFromBasket(int? id)
        {
            if (id is null) return BadRequest();

            List<BasketVM> basketProducts = new();


            if (_accessor.HttpContext.Request.Cookies["basket"] is not null)
                basketProducts = JsonConvert.DeserializeObject<List<BasketVM>>(_accessor.HttpContext.Request.Cookies["basket"]);



            basketProducts = basketProducts.Where(m => m.Id != id).ToList();

            _accessor.HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(basketProducts));


            int count = basketProducts.Sum(m => m.Count);
            decimal total = basketProducts.Sum(m => m.Count * m.Price);


            return Ok(new { count, total });

        }




        
    }
}
