using CRM_Project.Constants;
using CRM_Project.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRM_Project.Repository
{
    public class RepositoryUsers
    {
        AppDbContext _dbContext;
        public RepositoryUsers(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<User>> GetUsersData()
        {
            return await _dbContext.Users.ToListAsync();
        }
        public async Task AddUserData(  string newEmail,
                                        string newPassword,
                                        string newSurname,
                                        string newName,
                                        string newPhone,
                                        DateTime newBirthaday,
                                        UserSex newSex,
                                        UserRole newRole)
        {
            User newUser = new User()
            {
                Email = newEmail,
                Password = newPassword,
                Surname = newSurname,
                Name = newName,
                Phone = newPhone,
                Birthday = newBirthaday,
                Sex = newSex,
                Role = newRole
            };

            _dbContext.Add(newUser);
            await _dbContext.SaveChangesAsync();
        }

        public async Task ChangeUserData(   int UserId,
                                            string changedEmail,
                                            string changedPassword,
                                            string changedSurname,
                                            string changedName,
                                            string changedPhone,
                                            DateTime changedBirthaday,
                                            UserSex changedSex,
                                            UserRole changedRole)
        {
            User changedUser = new User()
            {
                Id = UserId,
                Email = changedEmail,
                Password = changedPassword,
                Surname = changedSurname,
                Name = changedSurname,
                Phone = changedPhone,
                Birthday = changedBirthaday,
                Sex = changedSex,
                Role = changedRole
            };

            _dbContext.Update(changedUser);
            await _dbContext.SaveChangesAsync();
        }
        public async Task RemoveUserData(int UserId)
        {
            _dbContext.Remove(_dbContext.Users.SingleOrDefault(c => c.Id == UserId));
            await _dbContext.SaveChangesAsync();
        }

        public async Task<User> LoginUser(string email,
                                                string password)
        {
            var user = await _dbContext.Users
                .Where(u => (u.Email == email)
                    && (u.Password == password))
                .SingleOrDefaultAsync();

            return user;
        }
    }
}
