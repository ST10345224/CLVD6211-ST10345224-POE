using KhumaloCraftPOE.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace KhumaloCraftPOE.Models
{
    public class Order
    {
        public int OrderId { get; set; } // Primary Key

        public string UserId { get; set; } // Foreign Key to User

        public virtual User User { get; set; } // Foreign Key to User

        public DateTime CreatedAt { get; set; }

        public string Status { get; set; } // Optional: Order Status

        public decimal Cost { get; set; } // Optional: Total Cost of Order

        public int Quantity { get; set; } // Optional: Total Quantity of Order

        public string ProductName { get; set; } // Optional: Product Name

        public ICollection<OrderItem> OrderItems { get; set; } // Navigation Property for OrderItems
    }
}
