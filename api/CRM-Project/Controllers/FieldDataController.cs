using CRM_Project.Constants;
using CRM_Project.Data;
using CRM_Project.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM_Project.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FieldDataController : ControllerBase
    {
        RepositoryFields _repository;
        public FieldDataController(RepositoryFields repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Authorize(Roles = "Seller")]
        public async Task<List<Field>> GetData()
        {
            return await _repository.GetFieldData();
        }

        [HttpPost]
        [Authorize(Roles = "Seller")]
        public async Task<OkResult> AddData([FromForm] int newOwnerId,
                                            [FromForm] string newName,
                                            [FromForm] string newLocation,
                                            [FromForm] double newSize)
        {
            await _repository.AddFieldData( newOwnerId,
                                            newName,
                                            newLocation,
                                            newSize);

            return Ok();
        }

        [HttpPut("{FieldId}")]
        [Authorize(Roles = "Seller")]
        public async Task<OkResult> ChangeData( int FieldId,
                                                [FromForm] int changedOwnerId,
                                                [FromForm] string changedName,
                                                [FromForm] string changedLocation,
                                                [FromForm] double changedSize)
        {
            await _repository.ChangeFieldData(  FieldId,
                                                changedOwnerId,
                                                changedName,
                                                changedLocation,
                                                changedSize);

            return Ok();
        }

        [HttpDelete("{FieldId}")]
        [Authorize(Roles = "Seller")]
        public async Task<OkResult> RemoveData(int FieldId)
        {
            await _repository.RemoveFieldData(FieldId);

            return Ok();
        }
    }
}
