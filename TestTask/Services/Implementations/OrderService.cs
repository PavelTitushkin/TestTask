using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TestTask.Data;
using TestTask.Models;
using TestTask.Models.ModelsConfig;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;
        public AppSettings AppSettings { get; set; }

        public OrderService(IOptions<AppSettings> options, ApplicationDbContext context)
        {
            _context = context;
            AppSettings = options.Value;
        }

        public Task<Order> GetOrder()
        {
            var order = _context.Orders
                .AsNoTracking()
                .OrderByDescending(o => o.Price * o.Quantity)
                .FirstOrDefaultAsync();

            return order;
        }

        public Task<List<Order>> GetOrders()
        {
            var orders = _context.Orders
               .AsNoTracking()
               .Where(o => o.Quantity > AppSettings.QuantityProduct)
               .ToListAsync();

            return orders;
        }
    }
}
