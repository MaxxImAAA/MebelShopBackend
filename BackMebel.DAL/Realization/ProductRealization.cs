using BackMebel.DAL.Data;
using BackMebel.DAL.Interfaces;
using BackMebel.Domain.Models.ProductModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackMebel.DAL.Realization
{
    public class ProductRealization : IProductInterface
    {
        private readonly ApplicationDbContext db;
        public ProductRealization(ApplicationDbContext db)
        {
            this.db = db;   
        }


        public async Task<bool> Create(Product model)
        {
            await db.Products.AddAsync(model);
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(Product model)
        {
            db.Products.Remove(model);
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<Product> Get(int id)
        {
            return await db.Products.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Product>> GetAll()
        {
            return await db.Products.ToListAsync();
        }

        public async Task<List<Product>> GetAllProductByName(string name)
        {
            return await db.Products.Where(x => x.Name.Contains(name)).ToListAsync();
        }

        public async Task<List<Product>> GetAllProductByOrderProduct(int userId)
        {
            var orders = await db.Orders.Where(x => x.UserId == userId).ToListAsync();
            List<Product> products = new List<Product>();
            var orderproducts = await  db.OrderProducts.Include(x => x.Product).ToListAsync();
            foreach(var item in orders)
            {
                foreach(var item2 in orderproducts)
                {
                    if(item2.OrderId == item.Id)
                    {
                        products.Add(item2.Product);
                    }
                }
               
            }

            return products;
        }

        public async Task<List<Product>> GetAllProductByType(string type)
        {
            return await db.Products.Where(x => x.ProductType == type).ToListAsync();
        }

        public async Task<List<Product>> GetAllProductsByCartProducts(int userId)
        {
            var cart =  await db.Carts.FirstOrDefaultAsync(x => x.UserId == userId);
            var cartproduscts = await db.CartProducts.Where(x => x.Cart == cart).Include(x=>x.Product).ToListAsync();
            List<Product> products = new List<Product>();
            foreach(var item in cartproduscts)
            {
                products.Add(item.Product);
            }
            return products;
        }
    }
}
