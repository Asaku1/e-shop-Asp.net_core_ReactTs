using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.RequestHelpers
{
    //PagedList<T> means PagedList<T> can be a type of T; generic type parameter.
    //Extend PagedList<T> into the  class List<T>
    public class PagedList<T> : List<T>
    {
        //Pass in a List of "items" along with "count", "pageNumber", & "pageSize" into "PagedList".
        //count = total number of products; pageSize = number of products per page.
        public PagedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            //***Create the "MetaData" object that we will return with the "PagedList"
            //***So "items" here is not the "MetaData" but we are returning every "items" inside the "PagedList"...
            //   together with the "Metadata".
            MetaData = new MetaData
            {
                TotalCount = count,
                PageSize = pageSize,
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize)
            };
            //the "items" is the Product called "query" in ProductController.cs that has undergone OrderBy, etc.
            //We add the "items" together with the "Metadata" and send it in the "PagedList"
            AddRange(items);
        }

        public MetaData MetaData { get; set; }


        //The method "ToPagedList" is a "PagedList<T> List".
        //"query" in "IQueryable<T> query" is made up of "items & MetaData"
        public static async Task<PagedList<T>> ToPagedList(IQueryable<T> query, int pageNumber, int pageSize)
        {
            var count = await query.CountAsync();
            var items = await query.Skip((pageNumber-1)*pageSize).Take(pageSize).ToListAsync();

            //PagedList contains "items" & "MetaData"
            return new PagedList<T>(items, count, pageNumber, pageSize);
        }
    }
}
