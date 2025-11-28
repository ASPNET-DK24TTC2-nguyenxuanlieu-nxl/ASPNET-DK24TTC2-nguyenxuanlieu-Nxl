using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shopbanhangapple.Data;

namespace Shopbanhangapple.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    [Route("Admin/[controller]")]
    public class CustomersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public CustomersController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Admin/Customers
        [HttpGet("")]
        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            
            var customerData = new List<CustomerViewModel>();
            
            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var orderCount = await _context.Orders
                    .Where(o => o.UserId == user.Id)
                    .CountAsync();
                var totalSpent = await _context.Orders
                    .Where(o => o.UserId == user.Id)
                    .SumAsync(o => (decimal?)o.TotalAmount) ?? 0;

                customerData.Add(new CustomerViewModel
                {
                    Id = user.Id,
                    Email = user.Email ?? "",
                    PhoneNumber = user.PhoneNumber ?? "",
                    EmailConfirmed = user.EmailConfirmed,
                    Roles = string.Join(", ", roles),
                    OrderCount = orderCount,
                    TotalSpent = totalSpent
                });
            }

            return View("~/Views/Admin/Customers/Index.cshtml", customerData);
        }

        // GET: Admin/Customers/Details/5
        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Details(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var roles = await _userManager.GetRolesAsync(user);
            var orders = await _context.Orders
                .Where(o => o.UserId == id)
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            var viewModel = new CustomerDetailsViewModel
            {
                Id = user.Id,
                Email = user.Email ?? "",
                PhoneNumber = user.PhoneNumber ?? "",
                EmailConfirmed = user.EmailConfirmed,
                Roles = string.Join(", ", roles),
                Orders = orders
            };

            return View("~/Views/Admin/Customers/Details.cshtml", viewModel);
        }
    }

    public class CustomerViewModel
    {
        public string Id { get; set; } = "";
        public string Email { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        public bool EmailConfirmed { get; set; }
        public string Roles { get; set; } = "";
        public int OrderCount { get; set; }
        public decimal TotalSpent { get; set; }
    }

    public class CustomerDetailsViewModel
    {
        public string Id { get; set; } = "";
        public string Email { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        public bool EmailConfirmed { get; set; }
        public string Roles { get; set; } = "";
        public List<Shopbanhangapple.Models.Order> Orders { get; set; } = new();
    }
}
