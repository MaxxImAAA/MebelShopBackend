using AutoMapper;
using BackMebel.DAL.Data;
using BackMebel.DAL.Interfaces;
using BackMebel.Domain.Models.OrderModels;
using BackMebel.Domain.Models.ProductModels;
using BackMebel.Domain.ServiceResponseModels;
using BackMebel.Service.Dtos.ProductDtos;
using BackMebel.Service.IService;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BackMebel.Service.Service
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;
        private readonly ICartProductInterface _cartproduct;
        private readonly IUserInterface _user;
        private readonly IOrderProductInterface _orderproduct;
        private readonly IOrderInterface _order;
        private readonly IProductInterface _product;
        private readonly IMapper mapper;
        public OrderService(ICartProductInterface _cartproduct, ApplicationDbContext _context, IMapper mapper, IUserInterface _user, IOrderProductInterface _orderproduct, IOrderInterface _order, IProductInterface _product)
        {
            this._cartproduct = _cartproduct;
            this._user = _user;
            this._orderproduct = _orderproduct;
            this._order = _order;
            this._product = _product;
            this.mapper = mapper;
            this._context = _context;

        }

        public async Task<ServiceResponse<bool>> AddOrder(int userId)
        {
            var service = new ServiceResponse<bool>();
            using(var transaction = _context.Database.BeginTransaction()) 
            {
                try
                {
                    var user = await _user.Get(userId);  // находим пользователя 

                    if(user.Cart.ProductCount == 0)
                    {
                        return new ServiceResponse<bool>()
                        {
                            Description = "В корзине нет товаров",
                            StatusCode = Domain.Enums.StatusCode.BadRequest
                        };
                    }

                    Order newOrder = new Order()  // Создание заказа у пользователя
                    {

                        User = user,
                        CreatedDate = DateTime.UtcNow,
                        OrderProducts = new List<OrderProduct>(),
                        CountProduct = user.Cart.ProductCount,

                    };


                    List<Product> products = await _product.GetAllProductsByCartProducts(userId); //выводим все продукты которые находятся в коризне у пользователя



                    List<OrderProduct> orderproducts = new List<OrderProduct>();  // выделение памяти для list orderproducts  который будет храниться в в объекте order
                    newOrder.OrderProducts = orderproducts;

                    foreach (var item in products)  // здесб идет добавление 
                    {
                        OrderProduct orderProduct = new OrderProduct()
                        {

                            Order = newOrder,
                            Product = item,
                        };


                        orderproducts.Add(orderProduct);

                    }

                    var resultorder = await _order.Create(newOrder);
                    bool resultCartproduct = false;
                    if (resultorder == true)
                    {

                        resultCartproduct = await _cartproduct.DeleteAllCartProductByUser(userId);

                    }

                    if (resultCartproduct == true)
                    {
                        transaction.Commit();
                        service.Description = "Заказ Оформлен";
                        service.StatusCode = Domain.Enums.StatusCode.OK;
                    }
                    else
                    {
                        service.Description = "Не удалось оформить заказ";
                        service.StatusCode = Domain.Enums.StatusCode.BadRequest;
                    }


                }
                catch (Exception ex)
                {
                   await transaction.RollbackAsync();
                    service.Description = $"[AddOrder] : {ex.Message}";
                    service.StatusCode = Domain.Enums.StatusCode.BadRequest;
                }
            }
           
            return service;
        } 

        public async Task<ServiceResponse<List<ProductDto>>> GetAllOrder(int userId)
        {
            var service = new ServiceResponse<List<ProductDto>>();

            try
            {
                var products = await _product.GetAllProductByOrderProduct(userId);
                if (products == null)
                {
                    service.Description = "У вас нет заказов";
                    service.StatusCode = Domain.Enums.StatusCode.OK;
                }
                else
                {
                    service.Description = "Выведены все ваши заказы";
                    service.StatusCode = Domain.Enums.StatusCode.OK;
                    service.Data = mapper.Map<List<ProductDto>>(products);
                }


            }
            catch (Exception ex)
            {
                service.Description = $"[GetAllOrder] : {ex.Message}";
                service.StatusCode = Domain.Enums.StatusCode.InternalServerError;
            }
            return service;
        } // добавить countproduct
    }
}
