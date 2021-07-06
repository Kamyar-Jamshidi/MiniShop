using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniShop.Api.Model.DTO;
using MiniShop.Api.Model.Present;
using MiniShop.Core.DTO;
using MiniShop.Core.DTO.Request;
using MiniShop.Core.DTO.Response;
using MiniShop.Core.Interfaces.Repositories;
using MiniShop.Core.UseCases;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiniShop.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IUserRepository _adminRepository;
        private readonly IProductRepository _productRepository;
        private readonly IProductCategoryRepository _productCategoryRepository;
        
        public ProductController(IUserRepository adminRepository,
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
        [Authorize(Policy = Role.PowerUser)]
        public async Task<IActionResult> ChangeProductStatus(ChangeProductStatusVM request)
        {
            try
            {
                var getAllProductsUseCase = new ChangeProductStatusUseCase(_productRepository, new Presenter<bool>());
                var result = await getAllProductsUseCase.HandleAsync(request.ProductId);
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest("Internal server error");
            }
        }

        [HttpPost("ProductForm")]
        [Authorize]
        public async Task<IActionResult> ProductForm(ProductFormVM request)
        {
            try
            {
                var productSaveUseCase = new ProductSaveUseCase(_productRepository, new Presenter<bool>());
                var result = await productSaveUseCase.HandleAsync(new ProductSaveRequest(request.Id, request.Title,
                    request.Description, request.IsTopRate, request.CategoryId));
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

        [HttpPost("GetCategories")]
        public async Task<IActionResult> GetCategories()
        {
            try
            {
                var getCategoriesUseCase = new GetCategoriesUseCase(_productCategoryRepository, new Presenter<List<GetCategoriesResponse>>());
                var result = await getCategoriesUseCase.HandleAsync();
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest("Internal server error");
            }
        }

        [HttpGet("GetCategory")]
        public async Task<IActionResult> GetCategory(int id)
        {
            try
            {
                var getCategoryUseCase = new GetCategoryUseCase(_productCategoryRepository, new Presenter<string>());
                var result = await getCategoryUseCase.HandleAsync(id);
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest("Internal server error");
            }
        }

        [HttpPost("CategoryForm")]
        [Authorize]
        public async Task<IActionResult> CategoryForm(CategoryFormVM request)
        {
            try
            {
                var categorySaveUseCase = new CategorySaveUseCase(_productCategoryRepository, new Presenter<bool>());
                var result = await categorySaveUseCase.HandleAsync(new CategorySaveRequest(request.Id, request.Title));
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest("Internal server error");
            }
        }
    }
}
