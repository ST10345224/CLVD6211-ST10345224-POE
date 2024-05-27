using KhumaloCraftPOE.Models;

namespace KhumaloCraftPOE.Repository
{
    public class OrderViewModel
    {
        public IEnumerable<Order> OrdersViewModel { get; set; }
        public IEnumerable<OrderItem> OrderItemsViewModel { get; set; }
        
    }
}
