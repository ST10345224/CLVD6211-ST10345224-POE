using KhumaloCraftPOE.Data;
using KhumaloCraftPOE.Models;
using Microsoft.EntityFrameworkCore;

namespace KhumaloCraftPOE.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly KhumaloCraftPOEDbContext _context;

        public OrderRepository(KhumaloCraftPOEDbContext context)
        {
            _context = context;
        }

        public Order GetOrderById(int orderId)
        {
            return _context.Orders.Find(orderId);
        }

        public Order GetUserOpenOrder(string userId)
        {
            return _context.Orders
                .Include(o => o.OrderItems) // Eager loading for OrderItems
                .FirstOrDefault(o => o.User.Id == userId && o.Status == "Open"); // Assuming "Open" status
        }

        public void Add(Order order)
        {
            _context.Orders.Add(order);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
