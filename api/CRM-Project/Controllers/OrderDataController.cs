using CRM_Project.Constants;
using CRM_Project.Data;
using CRM_Project.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CRM_Project.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrderDataController : ControllerBase
    {
        RepositoryOrders _repository;
        public OrderDataController(RepositoryOrders repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Authorize]
        public async Task<List<Order>> GetData()
        {
            return await _repository.GetOrderData();
        }

        [HttpPost]
        [Authorize(Roles = "Seller")]
        public async Task<OkResult> AddData([FromBody] OrderDTO orderDTO)
        {
            await _repository.AddOrderData(orderDTO);

            return Ok();
        }

        [HttpPut("{OrderId}")]
        [Authorize]
        public async Task<OkResult> ChangeData( int OrderId,
                                                [FromBody] OrderDTO orderDTO)
        {
            await _repository.ChangeOrderData(OrderId, orderDTO);

            return Ok();
        }

        [HttpDelete("{OrderId}")]
        [Authorize(Roles = "Seller")]
        public async Task<OkResult> RemoveData(int OrderId)
        {
            await _repository.RemoveOrdeData(OrderId);

            return Ok();
        }
    }
}
