namespace API.DTOs
{
    public class BasketItemDto
    {
        public int ProductId { get; set; }

        //Fields found in Product.cs
        public string Name { get; set; }

        public string Description { get; set; }

        public long Price { get; set; }

        public string PictureUrl { get; set; }

        public string Type { get; set; }

        public string Brand { get; set; }
        //End of Fields found in Product.cs

        public int Quantity { get; set; }
    }
}