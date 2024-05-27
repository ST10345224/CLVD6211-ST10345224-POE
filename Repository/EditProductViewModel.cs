namespace KhumaloCraftPOE.Repository
{
    public class EditProductViewModel
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Category { get; set; } // You can include other relevant properties
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }
        public string? ImageUrl { get; set; } // nullable for image handling
    }
}

