namespace KhumaloCraftPOE.Models
{
    public class OrderItem
    {
        public int OrderItemId { get; set; } // Primary Key

        public int OrderId { get; set; } // Foreign Key to Order

        public Order Order { get; set; } // Navigation Property for Order

        public int ProductId { get; set; } // Foreign Key to Product

        public Product Product { get; set; } // Navigation Property for Product

        public int Quantity { get; set; }
    }
}
