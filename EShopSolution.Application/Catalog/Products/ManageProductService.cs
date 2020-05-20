using EShopSolution.Data.EF;
using EShopSolution.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShopSolution.Custom.Exceptions;
using Microsoft.EntityFrameworkCore;
using EShopSolution.ViewModels.Common;
using EShopSolution.ViewModels.Catalog.Product;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.IO;
using EShopSolution.Application.Common;
using Microsoft.AspNetCore.Hosting;

namespace EShopSolution.Application.Catalog.Products
{
    public class ManageProductService : IManageProductService
    {
        private readonly EShopDbContext _context;
        private readonly IStorageService _storageService;
        //[Obsolete]
        //private IHostingEnvironment _env;

        //private string _dir;

        [Obsolete]
        public ManageProductService(EShopDbContext context, IStorageService storageService) //IHostingEnvironment env)
        {
            _context = context;
            _storageService = storageService;
            //_env = env;
            //_dir = _env.ContentRootPath;
        }

        public Task<int> AddImages(int productId, List<IFormFile> files)
        {
            /*var product = await _context.Products.FindAsync(productId);
            if (product == null) throw new EShopException($"Cannot find product with id : {productId}");

            if (files != null)
            {
                int i = 0;
                foreach (var file in files)
                {
                    using (var fileStream = new FileStream(Path.Combine(_dir,$"file{i++}.png"), FileMode.Create,FileAccess.Write))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                }
            }
            return await _context.SaveChangesAsync();
            */
            throw new NotImplementedException();


        }

        public async Task AddViewcount(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            product.ViewCount += 1;
            await _context.SaveChangesAsync();
        }

        public async Task<int> Create(ProductCreateRequest request)
        {
            var product = new Product()
            {
                Price = request.Price,
                OriginalPrice = request.OriginalPrice,
                Stock = request.Stock,
                ViewCount = 0,
                DateCreated = DateTime.Now,
                ProductTranslations = new List<ProductTranslation>()
                {
                    new ProductTranslation()
                    {
                        Name = request.Name,
                        Description = request.Description,
                        Details = request.Details,
                        SeoAlias = request.SeoAlias,
                        SeoDescription= request.SeoDescription,
                        SeoTitle= request.SeoTitle,
                        LanguageId = request.LanguageId
                    }
                }
            };
            //Save Image
            if(request.ThumbnailImage != null)
            {
                product.ProductImages = new List<ProductImage>()
                {
                    new ProductImage()
                    {
                        Caption = "Thumbnail image",
                        DateCreated = DateTime.Now,
                        FileSize = request.ThumbnailImage.Length,
                        ImagePath = await this.SaveFile(request.ThumbnailImage),
                        IsDefault = true,
                        SortOrder = 1
                    }
                };
            }
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return product.Id;

        }

        public async Task<int> Delete(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) throw new EShopException($"cannot find a product: {productId}");

            var images = _context.ProductImages.Where(i => i.productId == productId);
            foreach(var image in images)
            {
                await _storageService.DeleteFileAsync(image.ImagePath);
            }
            _context.Products.Remove(product);

            
            return await _context.SaveChangesAsync();
        }



        public async Task<PagedResult<ProductViewModel>> GetAllPaging(GetManageProductPagingRequest request)
        {
            //1. Select join
            var query = from p in _context.Products
                        join pt in _context.ProductTranslations on p.Id equals pt.ProductId
                        join pic in _context.productInCategories on p.Id equals pic.ProductId
                        join c in _context.Categories on pic.CategoryId equals c.Id
                        select new { p, pt, pic };

            //2. Filter
            if (!string.IsNullOrEmpty(request.keyword))
                query = query.Where(x => x.pt.Name.Contains(request.keyword));
            if (request.CategoryIds.Count > 0)
            {
                query = query.Where(p => request.CategoryIds.Contains(p.pic.CategoryId));
            }
            //3. Paging
            int totalRow = await query.CountAsync();
            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new ProductViewModel()
                {
                    Id = x.p.Id,
                    Name = x.pt.Name,
                    DateCreated = x.p.DateCreated,
                    Description = x.pt.Description,
                    Details = x.pt.Details,
                    LanguageId = x.pt.LanguageId,
                    OriginalPrice = x.p.OriginalPrice,
                    Price = x.p.Price,
                    SeoAlias = x.pt.SeoAlias,
                    SeoDescription = x.pt.SeoDescription,
                    SeoTitle = x.pt.SeoTitle,
                    Stock = x.p.Stock,
                    ViewCount = x.p.ViewCount
                }).ToListAsync();
            //4. Select and projection
            var pageResult = new PagedResult<ProductViewModel>
            {
                TotalRecord = totalRow,
                Items = data
            };

            return pageResult;
        }

        public async Task<ProductViewModel> GetById(int productId, string LanguageId)
        {
            var product = await _context.Products.FindAsync(productId);
            var productTranlation = await _context.ProductTranslations.FirstOrDefaultAsync(x => x.ProductId == productId && x.LanguageId == LanguageId);

            var productViewModel = new ProductViewModel()
            {
                Id = product.Id,
                Description = productTranlation != null ? productTranlation.Description : null,
                Details = productTranlation != null ? productTranlation.Details : null,
                SeoDescription = productTranlation != null ? productTranlation.SeoDescription : null,
                LanguageId = productTranlation.LanguageId,
                Name = productTranlation != null ? productTranlation.Name : null,
                OriginalPrice = product.OriginalPrice,
                Price = product.Price,
                SeoAlias = productTranlation != null ? productTranlation.SeoAlias : null,
                SeoTitle = productTranlation != null ? productTranlation.SeoTitle : null,
                Stock = product.Stock,
                ViewCount=product.ViewCount,
                DateCreated = product.DateCreated,
            };

            return productViewModel;
            
        }

        public Task<List<ProductImageViewModel>> GetListImage(int productId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdatePrice(int productId, decimal newPrice)
        {
            var product = await _context.Products.FindAsync(productId);
            
            if (product == null ) throw new EShopException($"cannot find a product with id: {productId}");

            product.Price = newPrice;
            return await _context.SaveChangesAsync() > 0;
        }

        public Task<int> RemoveImages(int imageId)
        {
            throw new NotImplementedException();
        }

        public async Task<int> Update(ProductUpdateRequest request)
        {
            var product = await _context.Products.FindAsync(request.Id);
            var productTranslations = await _context.ProductTranslations.FirstOrDefaultAsync(x => x.ProductId == request.Id && x.LanguageId == request.LanguageId);
            if (product == null || productTranslations == null) throw new EShopException($"cannot find a product with id: {request.Id}");

            productTranslations.Name = request.Name;
            productTranslations.SeoAlias = request.SeoAlias;
            productTranslations.SeoDescription = request.SeoDescription;
            productTranslations.SeoTitle = request.SeoTitle;
            productTranslations.Details = request.Details;
            productTranslations.Description = request.Description;

            //Save Image
            if (request.ThumbnailImage != null)
            {
                var thumbnailImage = await _context.ProductImages.FirstOrDefaultAsync(i => i.IsDefault == true && i.productId == request.Id);
                if(thumbnailImage != null)
                {
                    thumbnailImage.FileSize = request.ThumbnailImage.Length;
                    thumbnailImage.ImagePath = await this.SaveFile(request.ThumbnailImage);
                    _context.ProductImages.Update(thumbnailImage);
                }
            }

            return await _context.SaveChangesAsync();
        }

        public Task<int> UpdateImage(int imageId, string caption, bool isDefault)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateStock(int productId, int addedQuantity)
        {
            var product = await _context.Products.FindAsync(productId);

            if (product == null) throw new EShopException($"cannot find a product with id: {productId}");

            product.Stock += addedQuantity;
            return await _context.SaveChangesAsync() > 0;
        }


        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }
    }
}
