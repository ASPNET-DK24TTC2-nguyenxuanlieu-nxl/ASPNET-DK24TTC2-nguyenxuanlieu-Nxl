using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shopbanhangapple.Data;
using Shopbanhangapple.Models;
using System.Security.Claims;
using System.Text.Json;

namespace Shopbanhangapple.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private const string CartSessionKey = "ShoppingCart";

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Orders
        [HttpGet("")]
        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orders = await _context.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            return View(orders);
        }

        // GET: Orders/Details/5
        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var order = await _context.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .FirstOrDefaultAsync(m => m.Id == id && m.UserId == userId);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Checkout
        [HttpGet("Checkout")]
        public IActionResult Checkout()
        {
            var cart = GetCart();
            if (cart.Count == 0)
            {
                return RedirectToAction("Index", "Cart");
            }

            return View(cart);
        }

        // POST: Orders/Checkout
        [HttpPost("Checkout")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(string shippingAddress, string paymentMethod)
        {
            var cart = GetCart();
            if (cart.Count == 0)
            {
                return RedirectToAction("Index", "Cart");
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }

            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.Now,
                Status = "Đã đặt hàng",
                ShippingAddress = shippingAddress,
                PaymentMethod = paymentMethod,
                TotalAmount = cart.Sum(c => c.Total),
                OrderDetails = cart.Select(c => new OrderDetail
                {
                    ProductId = c.ProductId,
                    Quantity = c.Quantity,
                    Price = c.Price,
                    Configuration = c.Configuration
                }).ToList()
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            // Clear cart
            HttpContext.Session.Remove(CartSessionKey);

            return Redirect($"/Orders/Details/{order.Id}");
        }

        private List<CartItem> GetCart()
        {
            var cartJson = HttpContext.Session.GetString(CartSessionKey);
            if (string.IsNullOrEmpty(cartJson))
            {
                return new List<CartItem>();
            }
            return JsonSerializer.Deserialize<List<CartItem>>(cartJson) ?? new List<CartItem>();
        }
    }
}
