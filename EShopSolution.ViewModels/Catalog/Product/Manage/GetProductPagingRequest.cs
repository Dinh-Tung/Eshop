using EShopSolution.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShopSolution.ViewModels.Catalog.Product.Manage
{
    public class GetProductPagingRequest : PagingRequestBase
    {
        public string keyword { get; set; }

        public List<int> CategoryIds { get; set; }
    }
}
