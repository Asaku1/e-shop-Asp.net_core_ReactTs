using API.Data;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.RequestHelpers;
using API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace API.Controllers
{

    public class ProductsController : BaseApiController
    {
        private readonly StoreContext _context;
        private readonly IMapper _mapper;
        private readonly ImageService _imageService;

        public ProductsController(StoreContext context, IMapper mapper, ImageService imageService)
        {
            _context = context;
            _mapper = mapper;
            _imageService = imageService;
        }

        [HttpGet]
        ////"Product" is from the "Product.cs"
        //// "_context.Products" is from the database
        ////The API can be seen from the name "ProductsController.cs": /api/Products
        public async Task<ActionResult<PagedList<Product>>> GetProducts([FromQuery]ProductParams productParams)
        {
            var query = _context.Products
                .Sort(productParams.OrderBy)
                .Search(productParams.SearchTerm)
                .Filter(productParams.Brands, productParams.Types)
                .AsQueryable();

            //The "Products" in "PagedList" are acted upon and manipulated by the method "ToPagedList"
            //***Recall: If we have: "public static Dog Bark {};" hence "public class Animal{var sound = Dog.Bark;}"
            //***        The class "Animal" accesses the static method "Dog"
            var products = await PagedList<Product>.ToPagedList(query, productParams.PageNumber, productParams.PageSize);

            Response.AddPaginationHeader(products.MetaData);

            return products;

            //return await _context.Products.ToListAsync(); //TableName = Products
        }



        [HttpGet("{id}", Name = "GetProduct")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id); //_context.Products.Find(id);

            if (product == null) return NotFound();

            return product;
        }


        [HttpGet("filters")]
        //Filter by Lists, i.e., we want to get two List of brands & types.
        //Here, we get only the brands and types in the list, we do not get names, etc.
        public async Task<IActionResult> GetFilters()
        {
            var brands = await _context.Products.Select(p => p.Brand).Distinct().ToListAsync();
            var types = await _context.Products.Select(p => p.Type).Distinct().ToListAsync();

            return Ok(new { brands, types });
        }

        //Method to Add a product to dB
        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct([FromForm]CreateProductDto productDto)
        {
            //mapping from "CreateProductDto" to "Product"
            var product = _mapper.Map<Product>(productDto); //mapping from "productDto" to "Product"

            if (productDto.File != null)
            {
                var imageResult = await _imageService.AddImageAsync(productDto.File);

                if (imageResult.Error != null) return BadRequest(new ProblemDetails { Title = imageResult.Error.Message });

                product.PictureUrl = imageResult.SecureUrl.ToString();
                product.PublicId = imageResult.PublicId;
            }

            _context.Products.Add(product);

            var result = await _context.SaveChangesAsync() > 0;

            if (result) return CreatedAtRoute("GetProduct", new {Id = product.Id}, product);

            return BadRequest(new ProblemDetails{ Title = "Problem creating new product" });

            //product.PictureUrl = imageResult.SecureUrl.ToString();
        }

        //Method to Update/Edit a product to dB
        [HttpPut]
        public async Task<ActionResult<Product>> UpdateProduct([FromForm]UpdateProductDto productDto)
        {
            var product = await _context.Products.FindAsync(productDto.Id);

            if (product == null) return NotFound(); //exit the "UpdateProduct" method if this is true.

            _mapper.Map(productDto, product); 

            if (productDto.File != null)
            {
                var imageResult = await _imageService.AddImageAsync(productDto.File);

                if (imageResult.Error != null)
                    return BadRequest(new ProblemDetails { Title = imageResult.Error.Message });

                if (!string.IsNullOrEmpty(product.PublicId)) 
                    await _imageService.DeleteImageAsync(product.PublicId);

                //if the imageResult was successful
                product.PictureUrl = imageResult.SecureUrl.ToString(); //set PictureUrl to the new url
                product.PublicId = imageResult.PublicId; //use the new PublicId
            }

            var result = await _context.SaveChangesAsync() > 0;

            if (result) return Ok(product);

            return BadRequest(new ProblemDetails { Title = "Problem updating product" });

        }

        //Method to Delete a product
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null) return NotFound();

            if (!string.IsNullOrEmpty(product.PublicId)) 
                await _imageService.DeleteImageAsync(product.PublicId);

            _context.Products.Remove(product);

            var result = await _context.SaveChangesAsync() > 0;

            if (result) return Ok();

            return BadRequest(new ProblemDetails { Title = "Problem deleting product" });
        }
    }
}
