using EShopSolution.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShopSolution.Application.Catalog.Products.Dtos.Manage
{
    public class GetProductPagingRequest : PagingRequestBase
    {
        public string keyword { get; set; }

        public List<int> CategoryIds { get; set; }
    }
}
