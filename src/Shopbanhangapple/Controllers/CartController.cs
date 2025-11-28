using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shopbanhangapple.Data;
using Shopbanhangapple.Models;
using System.Text.Json;

namespace Shopbanhangapple.Controllers
{
    [Route("[controller]")]
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;
        private const string CartSessionKey = "ShoppingCart";

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("")]
        [HttpGet("Index")]
        public IActionResult Index()
        {
            var cart = GetCart();
            return View(cart);
        }

        [HttpPost("AddToCart")]
        public async Task<IActionResult> AddToCart(int productId, string? configuration, int quantity = 1)
        {
            var product = await _context.Products
                .Include(p => p.Configurations)
                .FirstOrDefaultAsync(p => p.Id == productId);
                
            if (product == null)
            {
                return NotFound();
            }

            // Calculate price with configuration adjustments
            decimal finalPrice = product.Price;
            
            if (!string.IsNullOrEmpty(configuration) && product.Configurations != null)
            {
                // Parse configuration string: "Dung lượng: 128GB, Màu sắc: Đen"
                var configParts = configuration.Split(',');
                foreach (var part in configParts)
                {
                    var trimmedPart = part.Trim();
                    // Find matching configuration
                    var matchingConfig = product.Configurations
                        .FirstOrDefault(c => trimmedPart.Contains(c.OptionValue));
                    
                    if (matchingConfig != null)
                    {
                        finalPrice += matchingConfig.PriceAdjustment;
                    }
                }
            }

            var cart = GetCart();
            var cartItem = cart.FirstOrDefault(c => c.ProductId == productId && c.Configuration == configuration);

            if (cartItem != null)
            {
                cartItem.Quantity += quantity;
            }
            else
            {
                cart.Add(new CartItem
                {
                    ProductId = productId,
                    ProductName = product.Name,
                    Price = finalPrice, // Use calculated price
                    Quantity = quantity,
                    Configuration = configuration ?? string.Empty,
                    ImageUrl = product.ImageUrl
                });
            }

            SaveCart(cart);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost("RemoveFromCart")]
        public IActionResult RemoveFromCart(int productId, string? configuration)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(c => c.ProductId == productId && c.Configuration == configuration);
            if (item != null)
            {
                cart.Remove(item);
                SaveCart(cart);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost("UpdateQuantity")]
        public IActionResult UpdateQuantity(int productId, string? configuration, int quantity)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(c => c.ProductId == productId && c.Configuration == configuration);
            if (item != null)
            {
                if (quantity > 0)
                {
                    item.Quantity = quantity;
                }
                else
                {
                    cart.Remove(item);
                }
                SaveCart(cart);
            }
            return RedirectToAction(nameof(Index));
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

        private void SaveCart(List<CartItem> cart)
        {
            var cartJson = JsonSerializer.Serialize(cart);
            HttpContext.Session.SetString(CartSessionKey, cartJson);
        }
    }
}
