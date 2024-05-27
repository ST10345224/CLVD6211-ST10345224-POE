namespace KhumaloCraftPOE.Models
{
    public class Product
    {
        public int ProductId { get; set; } // Primary Key

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string? ImageUrl { get; set; }

        public string Category { get; set; } 

        public bool IsAvailable { get; set; }

    }
}
