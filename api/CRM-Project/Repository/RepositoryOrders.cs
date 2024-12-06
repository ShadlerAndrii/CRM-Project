using CRM_Project.Constants;
using CRM_Project.Data;
using Microsoft.EntityFrameworkCore;

namespace CRM_Project.Repository
{
    public class RepositoryOrders
    {
        AppDbContext _dbContext;
        public RepositoryOrders(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Order>> GetOrderData()
        {
            return await _dbContext.Orders.ToListAsync();
        }

        public async Task AddOrderData(OrderDTO orderDTO)
        {
            Order newOrder = new Order()
            {
                ByuerId = orderDTO.ByuerId,
                SellerId = orderDTO.SellerId,
                CultureId = orderDTO.CultureId,
                OrderAmount = orderDTO.OrderAmount,
                OrderPrice = orderDTO.OrderPrice
            };

            _dbContext.Orders.Add(newOrder);
            await _dbContext.SaveChangesAsync();
        }

        public async Task ChangeOrderData(int OrderId, OrderDTO orderDTO)
        {
            Order changedOrder = new Order()
            {
                Id = OrderId,
                ByuerId = orderDTO.ByuerId,
                SellerId = orderDTO.SellerId,
                CultureId = orderDTO.CultureId,
                OrderAmount = orderDTO.OrderAmount,
                OrderPrice = orderDTO.OrderPrice
            };

            _dbContext.Orders.Update(changedOrder);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveOrdeData(int OrderId)
        {
            _dbContext.Remove(_dbContext.Orders.SingleOrDefault(c => c.Id == OrderId));
            await _dbContext.SaveChangesAsync();
        }
    }
}
