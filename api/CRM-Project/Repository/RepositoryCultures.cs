using CRM_Project.Constants;
using CRM_Project.Data;
using Microsoft.EntityFrameworkCore;

namespace CRM_Project.Repository
{
    public class RepositoryCultures
    {
        AppDbContext _dbContext;
        public RepositoryCultures(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Culture>> GetCultureData()
        {
            return await _dbContext.Cultures.ToListAsync();
        }

        public async Task AddCultureData(   int OwnerId,
                                            CultureName newName,
                                            double newAmount,
                                            DateTime newHarvestDate)
        {
            Culture newCulture = new Culture()
            {
                OwnerId = OwnerId,
                Name = newName,
                Amount = newAmount,
                HarvestDate = newHarvestDate
            };

            _dbContext.Add(newCulture);
            await _dbContext.SaveChangesAsync();
        }

        public async Task ChangeCultureData(int CultureId,
                                            int OwnerId,
                                            CultureName changedName,
                                            double changedAmount,
                                            DateTime changedHarvestDate)
        {
            Culture changedCulture = new Culture()
            {
                Id = CultureId,
                OwnerId = OwnerId,
                Name = changedName,
                Amount = changedAmount,
                HarvestDate = changedHarvestDate
            };

            _dbContext.Update(changedCulture);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveCultureData(int CultureId)
        {
            _dbContext.Remove(_dbContext.Cultures.SingleOrDefault(c => c.Id == CultureId));
            await _dbContext.SaveChangesAsync();
        }
    }
}
