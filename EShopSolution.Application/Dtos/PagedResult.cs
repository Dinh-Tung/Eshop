using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace EShopSolution.Application.Dtos
{
    public class PagedResult<T>
    {
        public int TotalRecord { get; set; }

        public List<T> Items { get; set; }
    }
}
