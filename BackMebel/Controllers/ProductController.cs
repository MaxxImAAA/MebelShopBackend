using BackMebel.Domain.ServiceResponseModels;
using BackMebel.Service.Dtos.ProductDtos;
using BackMebel.Service.IService;
using BackMebel.Service.Tools.TokenFolder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BackMebel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {  //Добавление Продукта, Вывод Всех Продуктов
        private readonly IProductService _productService;
        private readonly TokenGetInfoInterface _token;
        public ProductController(IProductService _productService, TokenGetInfoInterface _token)
        {
            this._productService = _productService;
            this._token = _token;
        }

        [HttpPost("Add")]
        public async Task<ActionResult<ServiceResponse<ProductDto>>> AddProduct(ProductDtoWithoutId productDto)
        {
            var response = await _productService.AddProduct(productDto);
            return Ok(response);
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<ProductDto>>>> GetAllProducts()
        {
            var response = await _productService.GetAllProducts();
            return Ok(response);
        }

        [HttpPost("AddProductToCart")]
        public async Task<ActionResult<ServiceResponse<ProductDto>>> AddProductToCart(int idUser, int idproduct)
        {
            var response = await _productService.AddProductToCart(idUser, idproduct);
            return Ok(response);
        }

        [HttpDelete("DeleteProductFromCart")]
        public async Task<ActionResult<ServiceResponse<ProductDto>>> DeleteProductFromCart(int idUser, int idProduct)
        {
            var response = await _productService.DeleteProductFromCart(idUser, idProduct);
            return Ok(response);
        }

        //[Authorize]
        [HttpGet("GetAllProductByCart")]
        public async Task<ActionResult<ServiceResponse<List<ProductDto>>>> GetAllProductByCart(int UserId)
        {
           // string token = Request.Headers["Authorization"].FirstOrDefault()?.Replace("Bearer ", "");
           // int idUser = _token.TokenGetUserId(token);

            var response = await _productService.GetAllProductByCart(UserId);
            return Ok(response);
        }

        [HttpGet("GetproductByType")]
        public async Task<ActionResult<ServiceResponse<List<ProductDto>>>> GetAllProductByType(string type)
        {
            var response = await _productService.GetAllProductByType(type);
            return Ok(response);
        }

        [HttpGet("GetProductByName")]
        public async Task<ActionResult<ServiceResponse<List<ProductDto>>>> GetAllProductByName(string? name)
        {
            var response = await _productService.GetAllProductByName(name);
            return Ok(response);
        }
    }
}
