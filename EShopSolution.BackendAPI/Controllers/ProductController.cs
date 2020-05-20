using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShopSolution.Application.Catalog.Products;
using EShopSolution.ViewModels.Catalog.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EShopSolution.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


    public class ProductController : ControllerBase
    {
        private readonly IPublicProductService _publicProductService;

        private readonly IManageProductService _manageProductService;

        public ProductController(IPublicProductService publicProductService, IManageProductService manageProductService)
        {
            _publicProductService = publicProductService;
            _manageProductService = manageProductService;
        }

        //http://localhost:port/product
        [HttpGet("{languageId}")]
        public async Task<IActionResult> Get(string languageId)
        {
            var product = await _publicProductService.GetAll(languageId);
            return Ok(product);
        }

        //http://localhost:port/product/public-paging
        [HttpGet("public-paging /{languageId}")]
        public async Task<IActionResult> Get([FromQuery]GetPublicProductPagingRequest request)
        {
            var product = await _publicProductService.GetAllByCategotyId(request);
            return Ok(product);
        }

        [HttpGet("{id}/{LanguageId}")]
        public async Task<IActionResult> GetById(int id, string LanguageId)
        {
            var product = await _manageProductService.GetById(id, LanguageId);
            if (product == null)
                return BadRequest("cannot find product ");
            return Ok(product);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromForm]ProductCreateRequest request)
        {
            var productId = await _manageProductService.Create(request);

            if (productId == 0)
                return BadRequest();
            var product = await _manageProductService.GetById(productId, request.LanguageId);


            return CreatedAtAction(nameof(GetById), new { id = productId }, product);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm]ProductUpdateRequest request)
        {
            var affectedResult = await _manageProductService.Update(request);

            if (affectedResult == 0)
                return BadRequest();
            return Ok();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _manageProductService.Delete(id);

            if (result == 0)
                return BadRequest();
            return Ok();
        }

        [HttpPut("price/{id}/{newPice}")]
        public async Task<IActionResult> UpdatePrice(int id, decimal newPrice)
        {
            var isSucessful = await _manageProductService.UpdatePrice(id,newPrice);

            if (isSucessful)
                return Ok();
            return BadRequest();
        }
    }
}