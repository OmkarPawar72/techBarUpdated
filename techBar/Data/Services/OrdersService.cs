﻿using Microsoft.EntityFrameworkCore;
using techBar.Models;

namespace techBar.Data.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly EcomDbContext _context;

        public OrdersService(EcomDbContext context)
        {
            _context = context;
        }

        public async Task<List<Orders>> GetOrdersByUserIdAndRoleAsync(string userId, string userRole)
        {
            var orders = await _context.Orders.Include(n=>n.OrderItems).ThenInclude(n => n.Productscategory).Include(n=>n.User).ToListAsync();

            if(userRole != "Admin")
            {
                orders = orders.Where(n=>n.UserId == userId).ToList();  
            }

            return orders;
        }

        public async Task StoreOrderAsync(List<ShoppingCartItem> items, string userId, string userEmailAddress)
        {
            var order = new Orders() 
            {
                UserId = userId,
                Email = userEmailAddress
            };
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            foreach(var item in items)
            {
                var orderItem = new OrderItem()
                {
                    Amount = item.Amount,
                    ProductsCategoryId = item.ProductsCategory.Id,
                    OrderId = order.OrderId,
                    Price = item.ProductsCategory.Price
                };
                await _context.OrderItems.AddAsync(orderItem);
            }
            await _context.SaveChangesAsync();
        }
    }
}
