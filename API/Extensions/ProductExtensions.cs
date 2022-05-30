using API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Extensions
{
    public static class ProductExtensions
    {
        //Sort method
        public static IQueryable<Product> Sort(this IQueryable<Product> query, string orderBy)
        {
            if (string.IsNullOrWhiteSpace(orderBy)) return query.OrderBy(p => p.Name);

            query = orderBy switch
            {
                //p = Product
                "price" => query.OrderBy(p => p.Price),
                "priceDesc" => query.OrderByDescending(p => p.Price),
                _ => query.OrderBy(p => p.Name)
            };

            return query;
        }

        //Search method
        public static IQueryable<Product> Search(this IQueryable<Product> query, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm)) return query;

            //Trim removes all leading and trailing white-space characters from the string
            //First convert the "searchTerm" string to lowercase
            var lowerCaseSearchTerm = searchTerm.Trim().ToLower();

            //p = Product; Compare all the "Names" with "lowerCaseSearchTerm"
            return query.Where(p => p.Name.ToLower().Contains(lowerCaseSearchTerm));

        }

        //Filter method
        //We shall create two Lists "brandList" & "typeList".
        //We will read "brands" & "types" into these Lists as comma separated List:
        public static IQueryable<Product> Filter(this IQueryable<Product> query, string brands, string types)
        {
            var brandList = new List<string>(); //created the list brandList
            var typeList = new List<string>(); //created the list typeList

            //if "brands" is not null nor empty
            if (!string.IsNullOrEmpty(brands))
                //"brands" are converted to lowercase and converted to a comma...
                //separated List and read into the other List "brandList"
                brandList.AddRange(brands.ToLower().Split(",").ToList());

            //if "types" is not null nor empty
            if (!string.IsNullOrEmpty(types))
                //"types" are converted to lowercase and converted to a comma...
                //separated List and read into the other List "typeList"
                typeList.AddRange(types.ToLower().Split(",").ToList());

            //"query = p"; "brandList.Count" counts the number of "Brand" inside "brandList".
            //"brandList.Contains(p.Brand.ToLower())" runs through brandList to see if it contains "Brand".
            //p.Brand = brandList.Brand.
            query = query.Where(p => brandList.Count == 0 || brandList.Contains(p.Brand.ToLower()));
            query = query.Where(p => typeList.Count == 0 || typeList.Contains(p.Type.ToLower()));

            return query;
        }
    }
}
