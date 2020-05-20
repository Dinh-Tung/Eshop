using EShopSolution.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShopSolution.ViewModels.Catalog.Product
{
    public class GetPublicProductPagingRequest : PagingRequestBase
    {

        public string LanguageId { get; set; }
        public int? CategoryId { get; set; }
    }
}
