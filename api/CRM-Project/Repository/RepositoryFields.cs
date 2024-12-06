using CRM_Project.Constants;
using CRM_Project.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRM_Project.Repository
{
    public class RepositoryFields
    {
        AppDbContext _dbContext;
        public RepositoryFields(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Field>> GetFieldData()
        {
            return await _dbContext.Fields.ToListAsync();
        }

        public async Task AddFieldData( int OwnerId,
                                        string newName,
                                        string newLocation,
                                        double newSize)
        {
            Field newFiled = new Field()
            {
                OwnerId = OwnerId,
                Name = newName,
                Location = newLocation,
                Size = newSize
            };

            _dbContext.Add(newFiled);
            await _dbContext.SaveChangesAsync();
        }

        public async Task ChangeFieldData(  int FieldId,
                                            int OwnerId,
                                            string changedName,
                                            string changedLocation,
                                            double changedSize)
        {
            Field changedFiled = new Field()
            {
                Id = FieldId,
                OwnerId = OwnerId,
                Name = changedName,
                Location = changedLocation,
                Size = changedSize
            };

            _dbContext.Update(changedFiled);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveFieldData(int FieldId)
        {
            _dbContext.Remove(_dbContext.Fields.SingleOrDefault(c => c.Id == FieldId));
            await _dbContext.SaveChangesAsync();
        }
    }
}
