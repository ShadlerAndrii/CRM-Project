using CRM_Project.Constants;
using CRM_Project.Data;
using Microsoft.EntityFrameworkCore;

namespace CRM_Project.Repository
{
    public class RepositoryEquipments
    {
        AppDbContext _dbContext;
        public RepositoryEquipments(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Equipment>> GetEquipmentData()
        {
            return await _dbContext.Equipments.ToListAsync();
        }

        public async Task AddEquipmentData( int OwnerId,
                                            string Model,
                                            EquipmentType Type)
        {
            Equipment newEquipment = new Equipment()
            {
                OwnerId = OwnerId,
                Model = Model,
                Type = Type
            };

            _dbContext.Equipments.Add(newEquipment);
            await _dbContext.SaveChangesAsync();
        }

        public async Task ChangeEquipmentData(  int EquipmentId,
                                                int OwnerId,
                                                string changedModel,
                                                EquipmentType changedType)
        {
            Equipment changedEquipment = new Equipment()
            {
                Id = EquipmentId,
                OwnerId = OwnerId,
                Model = changedModel,
                Type = changedType
            };

            _dbContext.Equipments.Update(changedEquipment);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveEquipmentData(int EquipmentId)
        {
            _dbContext.Remove(_dbContext.Equipments.SingleOrDefault(c => c.Id == EquipmentId));
            await _dbContext.SaveChangesAsync();
        }
    }
}
