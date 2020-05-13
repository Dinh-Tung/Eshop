using EShopSolution.ViewModels.Catalog.Product;
using EShopSolution.ViewModels.Catalog.Product.Public;
using EShopSolution.ViewModels.Common;
using System.Threading.Tasks;

namespace EShopSolution.Application.Catalog.Products
{
    public interface IPublicProductService
    {
        Task<PagedResult<ProductViewModel>> GetAllByCategotyId(GetProductPagingRequest request);
    }
}
