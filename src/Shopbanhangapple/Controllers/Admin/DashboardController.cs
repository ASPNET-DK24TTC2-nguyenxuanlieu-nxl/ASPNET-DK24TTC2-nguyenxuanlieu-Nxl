using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shopbanhangapple.Data;

namespace Shopbanhangapple.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    [Route("Admin")]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin
        [Route("")]
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            ViewBag.TotalProducts = await _context.Products.CountAsync();
            ViewBag.TotalOrders = await _context.Orders.CountAsync();
            ViewBag.TotalRevenue = await _context.Orders.SumAsync(o => o.TotalAmount);
            ViewBag.PendingOrders = await _context.Orders.CountAsync(o => o.Status == "Đã đặt hàng");
            ViewBag.LowStockProducts = await _context.Products.CountAsync(p => p.Stock < 10);

            var recentOrders = await _context.Orders
                .Include(o => o.OrderDetails)
                .OrderByDescending(o => o.OrderDate)
                .Take(10)
                .ToListAsync();

            return View("~/Views/Admin/Dashboard/Index.cshtml", recentOrders);
        }
    }
}
