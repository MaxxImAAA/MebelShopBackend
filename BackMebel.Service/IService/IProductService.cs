using BackMebel.Domain.ServiceResponseModels;
using BackMebel.Service.Dtos.ProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackMebel.Service.IService
{
    public interface IProductService
    {
        Task<ServiceResponse<ProductDto>> AddProduct(ProductDtoWithoutId productDto);

        Task<ServiceResponse<List<ProductDto>>> GetAllProducts();

        Task<ServiceResponse<ProductDto>> AddProductToCart(int idUser, int idproduct); //добавление товара в корзину пользователя

        Task<ServiceResponse<ProductDto>> DeleteProductFromCart(int iduser, int idproduct);

        Task<ServiceResponse<List<ProductDto>>> GetAllProductByCart(int idUser);

        Task<ServiceResponse<List<ProductDto>>> GetAllProductByType(string type);

        Task<ServiceResponse<List<ProductDto>>> GetAllProductByName(string name);


    }
}
