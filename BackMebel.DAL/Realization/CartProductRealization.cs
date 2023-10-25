using BackMebel.DAL.Data;
using BackMebel.DAL.Interfaces;
using BackMebel.Domain.Models.CartModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackMebel.DAL.Realization
{
    public class CartProductRealization : ICartProductInterface
    {
        private readonly ApplicationDbContext db;
        public CartProductRealization(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<bool> Create(CartProduct model)
        {
            await db.CartProducts.AddAsync(model);
           await db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(CartProduct model)
        {
            db.CartProducts.Remove(model);
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAllCartProductByUser(int userId)
        {
            var cart = await db.Carts.FirstOrDefaultAsync(x => x.UserId == userId);
            var cartproducts = await db.CartProducts.Where(x => x.Cart == cart).ToListAsync();
            foreach(var item in cartproducts)
            {
                db.CartProducts.Remove(item);
                cart.ProductCount--;
                
            }
            await db.SaveChangesAsync();
            return true;
        }

        public Task<CartProduct> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<CartProduct>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<CartProduct> GetCartProduct(int userId, int productId)
        {
            var cart = await db.Carts.FirstOrDefaultAsync(x => x.UserId == userId);
            var product = await db.Products.FirstOrDefaultAsync(x => x.Id == productId);
            var cartproduct = await db.CartProducts.FirstOrDefaultAsync(x => x.Cart == cart && x.Product == product);
            return cartproduct;
        }

        public async Task<List<CartProduct>> GetCartProducts(int userId)
        {
            var cart = await db.Carts.FirstOrDefaultAsync(x=>x.UserId == userId);

            return await db.CartProducts.Where(x => x.Cart == cart).Include(x=>x.Product).ToListAsync();
        }
    }
}
