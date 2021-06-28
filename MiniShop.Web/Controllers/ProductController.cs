using Microsoft.AspNetCore.Mvc;
using MiniShop.Core.DTO.Request;
using MiniShop.Core.DTO.Response;
using MiniShop.Core.Interfaces.Repositories;
using MiniShop.Core.UseCases;
using MiniShop.Web.DTO;
using MiniShop.Web.Present;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiniShop.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IProductRepository _productRepository;
        private readonly IProductCategoryRepository _productCategoryRepository;

        public ProductController(IAdminRepository adminRepository,
            IProductRepository productRepository,
            IProductCategoryRepository productCategoryRepository)
        {
            _adminRepository = adminRepository;
            _productRepository = productRepository;
            _productCategoryRepository = productCategoryRepository;
        }

        [HttpPost("GetAllProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                var getAllProductsUseCase = new GetAllProductsUseCase(_productRepository, new Presenter<List<GetProductsResponse>>());
                var result = await getAllProductsUseCase.HandleAsync(GetAllProductsUseCase.ProductTypes.All);
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest("Internal server error");
            }
        }

        [HttpPost("GetNewProducts")]
        public async Task<IActionResult> GetNewProducts()
        {
            try
            {
                var getAllProductsUseCase = new GetAllProductsUseCase(_productRepository, new Presenter<List<GetProductsResponse>>());
                var result = await getAllProductsUseCase.HandleAsync(GetAllProductsUseCase.ProductTypes.New);
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest("Internal server error");
            }
        }

        [HttpPost("GetTopProducts")]
        public async Task<IActionResult> GetTopProducts()
        {
            try
            {
                var getAllProductsUseCase = new GetAllProductsUseCase(_productRepository, new Presenter<List<GetProductsResponse>>());
                var result = await getAllProductsUseCase.HandleAsync(GetAllProductsUseCase.ProductTypes.Top);
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest("Internal server error");
            }
        }

        [HttpPost("ChangeProductStatus")]
        public async Task<IActionResult> ChangeProductStatus(ChangeProductStatusVM request)
        {
            try
            {
                var getAllProductsUseCase = new ChangeProductStatusUseCase(_adminRepository, _productRepository, new Presenter<bool>());
                var result = await getAllProductsUseCase.HandleAsync(new ChangeProductStatusRequest(request.Token, request.ProductId));
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest("Internal server error");
            }
        }

        [HttpPost("GetCategories")]
        public async Task<IActionResult> GetCategories(TokenVM request)
        {
            try
            {
                var getCategoriesUseCase = new GetCategoriesUseCase(_productCategoryRepository, new Presenter<List<GetCategoriesResponse>>());
                var result = await getCategoriesUseCase.HandleAsync(request.Token);
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest("Internal server error");
            }
        }

        [HttpPost("ProductForm")]
        public async Task<IActionResult> ProductForm(ProductForm request)
        {
            try
            {
                var productSaveUseCase = new ProductSaveUseCase(_adminRepository, _productRepository, new Presenter<bool>());
                var result = await productSaveUseCase.HandleAsync(new ProductSaveRequest(request.Token, request.Id, request.Title,
                    request.Description, request.CategoryId, request.IsTopRate));
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest("Internal server error");
            }
        }

        [HttpGet("GetProduct")]
        public async Task<IActionResult> GetProduct(int id)
        {
            try
            {
                var getProductUseCase = new GetProductUseCase(_productRepository, new Presenter<GetProductsResponse>());
                var result = await getProductUseCase.HandleAsync(id);
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest("Internal server error");
            }
        }

        [HttpGet("LikeProduct")]
        public async Task<IActionResult> LikeProduct(int id, bool like)
        {
            try
            {
                var getProductUseCase = new LikeDislikeProductUseCase(_productRepository, new Presenter<bool>());
                var result = await getProductUseCase.HandleAsync(new LikeDislikeProductRequest(id, like));
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest("Internal server error");
            }
        }
    }
}
