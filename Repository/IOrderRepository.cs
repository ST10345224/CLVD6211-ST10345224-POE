using KhumaloCraftPOE.Models;

namespace KhumaloCraftPOE.Repository;
public interface IOrderRepository
{
    // Get order by ID
    Order GetOrderById(int orderId);

    // Get user's open order (optional: define "Open" status in Order model)
    Order GetUserOpenOrder(string userId);

    // Add a new order
    void Add(Order order);

    // Save changes to order
    void Save();
}
