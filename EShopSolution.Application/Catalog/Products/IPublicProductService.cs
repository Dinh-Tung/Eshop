using EShopSolution.Application.Catalog.Products.Dtos;
using EShopSolution.Application.Catalog.Products.Dtos.Public;
using EShopSolution.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EShopSolution.Application.Catalog.Products
{
    public interface IPublicProductService
    {
        Task<PagedResult<ProductViewModel>> GetAllByCategotyId(GetProductPagingRequest request);
    }
}
