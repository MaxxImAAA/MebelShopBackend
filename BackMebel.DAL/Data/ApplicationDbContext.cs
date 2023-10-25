using BackMebel.Domain.Models.CartModels;
using BackMebel.Domain.Models.FeedBackModels;
using BackMebel.Domain.Models.MessageModels;
using BackMebel.Domain.Models.OrderModels;
using BackMebel.Domain.Models.ProductModels;
using BackMebel.Domain.Models.UserModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BackMebel.DAL.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        
        public DbSet<UserAuth> UserAuths { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<Order> Orders { get; set; }       
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartProduct> CartProducts { get;set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<UserAuth>() //Связь между UserAuth и User  1 к 1
                .HasOne(x => x.User).WithOne(x => x.UserAuth).HasForeignKey<User>(x => x.UserAuthId);

            modelBuilder.Entity<User>() //Связб между User и Orders One to many
                .HasMany(x => x.Orders).WithOne(x => x.User).HasForeignKey(x => x.UserId);

            modelBuilder.Entity<User>()
                .HasOne(x => x.Cart).WithOne(x => x.User).HasForeignKey<Cart>(x => x.UserId);

            modelBuilder.Entity<User>()
                .HasMany(x => x.Feedbacks).WithOne(x => x.User).HasForeignKey(x => x.UserId);

            //----------------------------------------------------------------------------------------------------
            //Связь многие ко многим Cart и Product  через промежуточную таблицу 
            modelBuilder.Entity<CartProduct>()
                .HasOne(x => x.Cart).WithMany(x => x.CartProducts).HasForeignKey(x => x.CartId);
                                                       
            modelBuilder.Entity<CartProduct>()
                .HasOne(x => x.Product).WithMany(x => x.CartProducts).HasForeignKey(x => x.ProductId);
            //----------------------------------------------------------------------------------------------------

            //----------------------------------------------------------------------------------------------------
            //Связь многие ко многим Order и Product  через промежуточную таблицу 
            modelBuilder.Entity<OrderProduct>()
                .HasOne(x => x.Order).WithMany(x => x.OrderProducts).HasForeignKey(x => x.OrderId);

            modelBuilder.Entity<OrderProduct>()
                .HasOne(x => x.Product).WithMany(x => x.OrderProducts).HasForeignKey(x => x.ProductId);
            //----------------------------------------------------------------------------------------------------

            modelBuilder.Entity<Message>()
                .HasOne(x => x.Sender).WithMany(x => x.Messages).HasForeignKey(x => x.SenderId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Message>()
               .HasOne(x => x.Reciver).WithMany().HasForeignKey(x => x.ReciverId).OnDelete(DeleteBehavior.Restrict);

        }
    }
}
