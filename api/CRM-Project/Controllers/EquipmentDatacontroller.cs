using CRM_Project.Constants;
using CRM_Project.Data;
using CRM_Project.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM_Project.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EquipmentDatacontroller : ControllerBase
    {
        RepositoryEquipments _repository;
        public EquipmentDatacontroller(RepositoryEquipments repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Authorize(Roles = "Seller")]
        public async Task<List<Equipment>> GetData()
        {
            return await _repository.GetEquipmentData();
        }

        [HttpPost]
        [Authorize(Roles = "Seller")]
        public async Task<OkResult> AddData([FromForm] int OwnerId,
                                            [FromForm] string Model,
                                            [FromForm] EquipmentType Type)
        {
            await _repository.AddEquipmentData( OwnerId,
                                                Model,
                                                Type);

            return Ok();
        }

        [HttpPut("{EquipmentId}")]
        [Authorize(Roles = "Seller")]
        public async Task<OkResult> ChangeData( [FromForm] int EquipmentId,
                                                [FromForm] int OwnerId,
                                                [FromForm] string changedModel,
                                                [FromForm] EquipmentType changedType)
        {
            await _repository.ChangeEquipmentData(  EquipmentId,
                                                    OwnerId,
                                                    changedModel,
                                                    changedType);

            return Ok();
        }

        [HttpDelete("{EquipmentId}")]
        [Authorize(Roles = "Seller")]
        public async Task<OkResult> RemoveData(int EquipmentId)
        {
            await _repository.RemoveEquipmentData(EquipmentId);

            return Ok();
        }
    }
}
