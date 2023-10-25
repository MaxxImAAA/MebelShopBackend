using AutoMapper;
using BackMebel.DAL.Interfaces;
using BackMebel.Domain.Models.CartModels;
using BackMebel.Domain.Models.ProductModels;
using BackMebel.Domain.Models.UserModels;
using BackMebel.Domain.ServiceResponseModels;
using BackMebel.Service.Dtos.ProductDtos;
using BackMebel.Service.IService;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace BackMebel.Service.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductInterface _productDal;
        private readonly IUserInterface _userDal;
        private readonly ICartProductInterface _cartproductDal;
        private readonly ICartInterface _cartDal;
        private readonly IMapper map;
        //r//r
        public ProductService(IProductInterface _productDal, ICartInterface _cartDal, IUserInterface _userDal, ICartProductInterface _cartproductDal, IMapper map)
        {
            this._productDal = _productDal;
            this._userDal = _userDal;
            this._cartproductDal = _cartproductDal;
            this._cartDal = _cartDal;
            this.map = map;
        }

        public async Task<ServiceResponse<ProductDto>> AddProduct(ProductDtoWithoutId productDto)
        {
            var service = new ServiceResponse<ProductDto>();
            try
            {
                Product newProduct = new Product()
                {
                    Name = productDto.Name,
                    ProductImage = productDto.ProductImage,
                    ShortDescription = productDto.ShortDescription,
                    Description = productDto.Description,
                    ProductType = productDto.ProductType,
                    Price = productDto.Price,
                };

                var result = await _productDal.Create(newProduct);

                if(result == true)
                {
                    service.Data = map.Map<ProductDto>(newProduct);
                    service.Description = " Продукт добавлен";
                    service.StatusCode = Domain.Enums.StatusCode.OK;
                }
                else
                {
                    service.Description = "Продукт не был добавлен";
                    service.StatusCode = Domain.Enums.StatusCode.BadRequest;
                }

            }
            catch (Exception ex)
            {
                service.Description = $"[Addproduct] : {ex.Message}";
                service.StatusCode = Domain.Enums.StatusCode.InternalServerError;
            }
            return service;
        }

        public async Task<ServiceResponse<ProductDto>> AddProductToCart(int idUser, int idproduct)
        {
            var service = new ServiceResponse<ProductDto>();
            try
            {
                var user = await _userDal.Get(idUser);

                var product = await _productDal.Get(idproduct);

                CartProduct newCartproduct = new CartProduct()
                {
                    Cart = user.Cart,
                    
                    
                    Product = product
                };
                user.Cart.ProductCount++;

                var result = await _cartproductDal.Create(newCartproduct);
                if(result == true)
                {
                    service.Description = "Продукт добавлен в корзину";
                    service.StatusCode = Domain.Enums.StatusCode.OK;
                }
                else
                {
                    service.Description = "Не удалось добавить продукт";
                    service.StatusCode = Domain.Enums.StatusCode.BadRequest;

                }

            }
            catch(Exception ex)
            {
                service.Description = $"[AddProductToCart] : {ex.Message}";
                service.StatusCode = Domain.Enums.StatusCode.BadRequest;
            }
            return service;
        }

        public async Task<ServiceResponse<ProductDto>> DeleteProductFromCart(int idUser, int idProduct)
        {
            var service = new ServiceResponse<ProductDto>();
            try
            {
                var cartproduct = await _cartproductDal.GetCartProduct(idUser, idProduct);
                var thiscart = await _cartDal.GetCartByUser(idUser);

                thiscart.ProductCount--;
                var result = await _cartproductDal.Delete(cartproduct);
                


                if (result == true)
                {
                    service.Description = "Продукт удален из вашей корзины";
                    service.StatusCode = Domain.Enums.StatusCode.OK;
                    
                }
                else
                {
                    service.Description = "Не удалось удалить продукт";
                    service.StatusCode = Domain.Enums.StatusCode.BadRequest;

                }

            }
            catch (Exception ex)
            {
                service.Description = $"[DeleteProductFromCart] : {ex.Message}";
                service.StatusCode = Domain.Enums.StatusCode.BadRequest;
            }
            return service;

        }

      

        public async Task<ServiceResponse<List<ProductDto>>> GetAllProductByCart(int idUser)
        {
            var service = new ServiceResponse<List<ProductDto>>();

            try
            {

                var user = await _userDal.Get(idUser);
                if(user.Cart.ProductCount == 0)
                {
                    return new ServiceResponse<List<ProductDto>>()
                    {
                        Description = "Ваша корзина товаров пуста",
                        StatusCode = Domain.Enums.StatusCode.NotFound,

                    };
                }
                else
                {
                    var products = await _productDal.GetAllProductsByCartProducts(idUser);

                    service.Data = map.Map<List<ProductDto>>(products);
                    service.Description = "Ваши товары выведены";
                    service.StatusCode = Domain.Enums.StatusCode.OK;
                }


            }
            catch(Exception ex)
            {
                service.Description = $"[GetAllProductByCart] : {ex.Message}";
                service.StatusCode = Domain.Enums.StatusCode.InternalServerError;
            }
            return service;
        }

        public async Task<ServiceResponse<List<ProductDto>>> GetAllProductByName(string? name)
        {
            var service = new ServiceResponse<List<ProductDto>>();
            try
            {
                var products = new List<Product>();
                if(name == null)
                {
                     products = await _productDal.GetAll();
                }
                else
                {
                    products = await _productDal.GetAllProductByName(name);
                }
               
                if(products.Count == 0)
                {
                    service.Description = "Товаров с таким именем нет";
                    service.StatusCode = Domain.Enums.StatusCode.NotFound;
                }
                else
                {
                    service.Data = map.Map<List<ProductDto>>(products);
                    service.Description = $"Выведены все товары с именем '{name}'";
                    service.StatusCode = Domain.Enums.StatusCode.OK;
                }
                

            }
            catch(Exception ex)
            {
                service.Description = $"[GetAllProductByName] : {ex.Message}";
                service.StatusCode = Domain.Enums.StatusCode.InternalServerError;
            }
            return service;
        }

        public async Task<ServiceResponse<List<ProductDto>>> GetAllProductByType(string type)
        {
            var service = new ServiceResponse<List<ProductDto>>();
            try
            {
                var products = await _productDal.GetAllProductByType(type);

                if(products == null)
                {
                    service.Description = "Товары не найдены";
                    service.StatusCode = Domain.Enums.StatusCode.NotFound;
                }
                else
                {
                    service.Data = map.Map<List<ProductDto>>(products);
                    service.Description = $"Товары категории '{type}' выведены";
                    service.StatusCode = Domain.Enums.StatusCode.OK;
                }


            }
            catch( Exception ex)
            {
                service.Description = $"[GetAllProductByType] : {ex.Message}";
                service.StatusCode = Domain.Enums.StatusCode.InternalServerError;
            }
            return service;
        }

        public async Task<ServiceResponse<List<ProductDto>>> GetAllProducts()
        {
            var service = new ServiceResponse<List<ProductDto>>();
            try
            {
                List<Product> newProducts = await _productDal.GetAll();
                if (newProducts == null)
                {
                    service.Description = "Товаров Нет";
                    service.StatusCode = Domain.Enums.StatusCode.NotFound;
                }
                else
                {
                    service.Data = map.Map<List<ProductDto>>(newProducts);
                    service.Description = "Выведены все продукты";
                    service.StatusCode = Domain.Enums.StatusCode.OK;
                }

            }
            catch(Exception ex)
            {
                service.Description = $"[GetAllProducts] : {ex.Message}";
                service.StatusCode = Domain.Enums.StatusCode.InternalServerError;
            }
            return service;
        }
    }
}
