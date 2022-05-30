using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class Basket
    {
        //Lesson 62

        public int Id { get; set; }

        public string BuyerId { get; set; }


        //// The List is called "BasketItem" and its contents are called "Items". 
        //// We can write the statement below as follows:
        //// public List<BasketItem> Items { get; set; } = new(); 
        //// ***Navigation properties where "Basket.cs(One)-to-(Many)BasketItem.cs"
        public List<BasketItem> Items { get; set; } = new List<BasketItem>(); //List<BasketItem> just brings BasketItem.cs inside Basket.cs


        // Add Item method: "product" & "quantity" are supplied by the customer
        public void AddItem(Product product, int quantity)
        {
            //Check if "one item" does not match any in "list of Items".
            //"product" is what the customer enters & "quantity" is the amount the customer enters.
            if(Items.All(item => item.ProductId != product.Id))
            {
                Items.Add(new BasketItem { Product = product, Quantity = quantity }); //this "item" is added to "Items"
            }

            //return the "item" the user passes to "item"; next initialize "existingItem" with "item".
            //Linq Query: "item" represents an entity inside "Items"
            //Linq Query: "item" can be replaced by "x".
            //var existingItem = Items.FirstOrDefault(x => x.ProductId == product.Id);
            var existingItem = Items.FirstOrDefault(item => item.ProductId == product.Id); //Linq Query

            //if there are "item" already the customer had, then just add the new one called "quantity"
            if (existingItem != null) existingItem.Quantity += quantity; //existingItem.Quantity = existingItem.Quantity + quantity

        }


        // Remove Item: "productId" and "quantity" and supplied by the customer
        public void RemoveItem(int productId, int quantity)
        {
            //Linq Query: "item" is an element inside "Items"; you can replace "item" with say "x".
            //Linq Query: var item = Items.FirstOrDefault(x => x.ProductId == productId);
            var item = Items.FirstOrDefault(item => item.ProductId == productId);

            if (item == null) return; //This statement exits out of this method if "item" is null

            item.Quantity -= quantity; // subtract "quantity" from  Quantity; item.Quantity = item.Quantity - quantity;

            //If there are no more "item.Quantity", then clear the "Items" Object; Items: {item, item, ...}
            if (item.Quantity == 0) Items.Remove(item);
        }
    }
}
