using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shopbanhangapple.Data;
using Shopbanhangapple.Models;

namespace Shopbanhangapple.Controllers
{
    [Route("[controller]")]
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Products
        [HttpGet("")]
        [HttpGet("Index")]
        public async Task<IActionResult> Index(int? categoryId)
        {
            var products = _context.Products.Include(p => p.Category).AsQueryable();
            
            if (categoryId.HasValue)
            {
                products = products.Where(p => p.CategoryId == categoryId.Value);
            }

            ViewBag.Categories = await _context.Categories.ToListAsync();
            return View(await products.ToListAsync());
        }

        // GET: Products/Details/5
        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Configurations)
                .FirstOrDefaultAsync(m => m.Id == id);
                
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
    }
}
