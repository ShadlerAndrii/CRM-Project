using CRM_Project.Constants;
using CRM_Project.Data;
using CRM_Project.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM_Project.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CultureDataController : ControllerBase
    {
        RepositoryCultures _repository;
        public CultureDataController(RepositoryCultures repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Authorize(Roles = "Seller")]
        public async Task<List<Culture>> GetData()
        {
            return await _repository.GetCultureData();
        }

        [HttpPost]
        [Authorize(Roles = "Seller")]
        public async Task<OkResult> AddData([FromForm] int OwnerId,
                                            [FromForm] CultureName newName,
                                            [FromForm] double newAmount,
                                            [FromForm] DateTime newHarvestDate)
        {
            await _repository.AddCultureData(   OwnerId,
                                                newName,
                                                newAmount,
                                                newHarvestDate);

            return Ok();
        }

        [HttpPut("{CultureId}")]
        [Authorize(Roles = "Seller")]
        public async Task<OkResult> ChangeData( int CultureId,
                                                [FromForm] int OwnerId,
                                                [FromForm] CultureName changedName,
                                                [FromForm] double changedAmount,
                                                [FromForm] DateTime changedHarvestDate)
        {
            await _repository.ChangeCultureData(CultureId,
                                                OwnerId,
                                                changedName,
                                                changedAmount,
                                                changedHarvestDate);

            return Ok();
        }

        [HttpDelete("{CultureId}")]
        [Authorize(Roles = "Seller")]
        public async Task<OkResult> RemoveData(int CultureId)
        {
            await _repository.RemoveCultureData(CultureId);

            return Ok();
        }
    }
}
