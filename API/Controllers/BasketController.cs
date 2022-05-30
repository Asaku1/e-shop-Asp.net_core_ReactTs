using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : BaseApiController
    {
        private readonly StoreContext _context;
        //private Basket basket;

        public BasketController(StoreContext context)
        {
            _context = context;
        }

        /*
        [HttpGet]

        public async Task<ActionResult<Basket>> GetBasket()
        {
            //TableName = Baskets
            //"Include(i => i.Items)" gets "Items" which is inside "Baskets". Use "Include" when the Object(Items) is inside "Basket.cs"
            //"ThenInclude(p => p.Product)" uses "ThenInclude" when the Object(Product) is not inside "Basket.cs" ...
            // but indirectly related to it by the navigation property.
            var basket = await _context.Baskets
                .Include(i => i.Items)
                .ThenInclude(p => p.Product)
                .FirstOrDefaultAsync(x => x.BuyerId == Request.Cookies["buyerId"]);

            if (basket == null) return NotFound();

            return basket;
        }
        */

        /* GetBasket method without using BasketDto
        [HttpGet]

        public async Task<ActionResult<Basket>> GetBasket()
        {
            //get basket
            var basket = await RetrieveBasket();

            if (basket == null) return NotFound();

            return basket;
        }
        */

        //GetBasket method using BasketDto
        [HttpGet(Name = "GetBasket")]

        public async Task<ActionResult<BasketDto>> GetBasket()
        {
            //get basket
            var basket = await RetrieveBasket();

            if (basket == null) return NotFound();

            return MapBasketToDto(basket);
        }

        
        [HttpPost] //api/basket?productId=3&quantity=2
        public async Task<ActionResult<BasketDto>> AddItemToBasket(int productId, int quantity)
        {
            //get basket
            var basket = await RetrieveBasket();

            //if basket is null, then create one.
            if (basket == null) basket = CreateBasket();

            var product = await _context.Products.FindAsync(productId); // TableName = Products 
                                                                        //Finds the "product" with the "productId"


            if (product == null) return BadRequest(new ProblemDetails {Title = "Product Not Found"}); //if "product" is not found, return "NotFound()" and this exits the method

            basket.AddItem(product, quantity); // "AddItem(product, quantity)" is from "Basket.cs"

            var result = await _context.SaveChangesAsync() > 0; // This goes through the dB table and save the products if its quantity of products is greater than zero.

            if (result) return CreatedAtRoute("GetBasket", MapBasketToDto(basket)); //"GetBasket" is from [HttpGet(Name = "GetBasket")]

            return BadRequest(new ProblemDetails{Title = "Problem saving item to basket"});
            
        }

       

        [HttpDelete]
        public async Task<ActionResult> RemoveBasketItem(int productId, int quantity)
        {
            // get basket 
            var basket = await RetrieveBasket();

            if (basket == null) return NotFound();

            basket.RemoveItem(productId, quantity); //"RemoveItem(productId, quantity)" is from Basket.cs

            var result = await _context.SaveChangesAsync() > 0;

            if (result) return Ok();

            return BadRequest(new ProblemDetails { Title = "Problem removing item from the basket" });
        }

        //Use this code in the GetBasketDto and AddItemToBasketDto methods
        private async Task<Basket> RetrieveBasket()
        {
            //TableName = Baskets
            //"Include(i => i.Items)" gets "Items" which is inside "Baskets". Use "Include" when the Object(Items) is inside "Basket.cs"
            //"ThenInclude(p => p.Product)" uses "ThenInclude" when the Object(Product) is not inside "Basket.cs" ...
            // but indirectly related to it by the navigation property.

            return await _context.Baskets
                .Include(i => i.Items)
                .ThenInclude(p => p.Product)
                .FirstOrDefaultAsync(x => x.BuyerId == Request.Cookies["buyerId"]);
        }

        private Basket CreateBasket()
        {
            //Create a randomly created "buyerId"
            var buyerId = Guid.NewGuid().ToString();
            var cookieOptions = new CookieOptions { IsEssential = true, Expires = DateTime.Now.AddDays(30) };
            Response.Cookies.Append("buyerId", buyerId, cookieOptions);
            var basket = new Basket { BuyerId = buyerId }; // creates new basket
            _context.Baskets.Add(basket); //basket object is added to TableName
            return basket;
        }

        //Mapping the Basket to BasketDto; We shall then use "MapBasketToDto" in "GetBasket" above.
        private BasketDto MapBasketToDto(Basket basket)
        {
            return new BasketDto
            {
                Id = basket.Id,
                BuyerId = basket.BuyerId,

                //get Items and all its properties as shown below.
                //Use Linq query inside "Select" statement
                Items = basket.Items.Select(item => new BasketItemDto
                {
                    ProductId = item.ProductId,
                    Name = item.Product.Name,
                    Price = item.Product.Price,
                    PictureUrl = item.Product.PictureUrl,
                    Type = item.Product.Type,
                    Brand = item.Product.Brand,
                    Quantity = item.Quantity
                }).ToList()
            };
        }
    }
}
