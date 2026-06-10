using GlossyCommerce.Data;
using GlossyCommerce.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;

namespace GlossyCommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrdersController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/orders (Used by Customer Checkout)
        [HttpPost]
        public async Task<ActionResult<Order>> PlaceOrder(Order order)
        {
            if (order == null || !order.OrderItems.Any())
            {
                return BadRequest("Order contains no items.");
            }

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            try
            {
                // FIX 1: Updated to the correct Gmail SMTP server
                using (var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new System.Net.NetworkCredential("Your Mail", "SMTP CODE"),
                    EnableSsl = true,
                })
                {
                    // 1. Email to Customer
                    var customerMail = new MailMessage("Your Mail", order.ContactEmail,
                        "Order Confirmation - GlossyCommerce",
                        $"Hi {order.CustomerName},\n\nYour order for ${order.TotalAmount:0.00} has been received and is now processing!");

                    // 2. Alert to Admin
                    var adminMail = new MailMessage("Your Mail", "Your Mail",
                        "New Order Received!",
                        $"New order #{order.Id} placed by {order.CustomerName} for ${order.TotalAmount:0.00}. Check the dashboard.");

                    // FIX 2: Removed the // slashes so the app actually sends the emails!
                    await smtpClient.SendMailAsync(customerMail);
                    await smtpClient.SendMailAsync(adminMail);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Email failed to send: {ex.Message}");
            }

            return Ok(order);
        }

        // GET: api/orders/dashboard (Used by Admin Dashboard for stats)
        [HttpGet("dashboard")]
        public async Task<IActionResult> GetDashboardStats()
        {
            var totalOrders = await _context.Orders.CountAsync();
            var totalRevenue = await _context.Orders.SumAsync(o => o.TotalAmount);

            // Assuming an arbitrary 30% margin for "profit generated" calculation
            var estimatedProfit = totalRevenue * 0.30m;

            var recentOrders = await _context.Orders
                .OrderByDescending(o => o.OrderDate)
                .Take(5)
                .ToListAsync();

            return Ok(new
            {
                TotalOrders = totalOrders,
                TotalRevenue = totalRevenue,
                EstimatedProfit = estimatedProfit,
                RecentOrders = recentOrders
            });
        }
    }
}