using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.RequestHelpers
{
    public class PaginationParams
    {
        private const int MaxPageSize = 50; //PageSize is the number of items per page.

        public int PageNumber { get; set; } = 1; // Page Number by default is the first page

        //The default page size is 6
        private int _pageSize = 6;

        //If user request a page size > 50 then return 50 Else return the value they asked. 
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
        }
    }
}
