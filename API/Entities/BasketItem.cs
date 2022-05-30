using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    [Table("BasketItems")]

    public class BasketItem
    {
        public int Id { get; set; }

        public int Quantity { get; set; }

        //// ***Navigation properties where "Product.cs(One)-to-(One)BasketItem.cs"
        public int ProductId { get; set; }

        public Product Product { get; set; }

        //// ***Navigation properties where "Basket.cs(One)-to-(One)BasketItem.cs"
        public int BasketId { get; set; }

        public Basket Basket { get; set; }

    }
}
