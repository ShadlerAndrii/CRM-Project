using CRM_Project.Constants;
using CRM_Project.Data;
using CRM_Project.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CRM_Project.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserDataController : ControllerBase
    {
        RepositoryUsers _repository;

        IConfiguration _configuration;

        public UserDataController(RepositoryUsers repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }

        private string Generate(string id, string name, string role)
        {
            var claims = new[]
            {
                new Claim("id", $"{id}"),
                new Claim("name", $"{name}"),
                new Claim("role", $"{role}")
            };

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var loginCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokenOptions = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: loginCredentials
            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return tokenString;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<List<User>> GetData()
        {
            return await _repository.GetUsersData();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<OkResult> AddData(    [FromForm] string newEmail,
                                                [FromForm] string newPassword,
                                                [FromForm] string newSurname,
                                                [FromForm] string newName,
                                                [FromForm] string newPhone,
                                                [FromForm] DateTime newBirthaday,
                                                [FromForm] UserSex newSex,
                                                [FromForm] UserRole newRole)
        {
            await _repository.AddUserData(  newEmail,
                                            newPassword,
                                            newSurname,
                                            newName,
                                            newPhone,
                                            newBirthaday,
                                            newSex,
                                            newRole);
            return Ok();
        }

        [HttpPut("{UserId}")]
        [Authorize]
        public async Task<OkResult> ChangeData( int UserId,
                                                [FromForm] string changedEmail,
                                                [FromForm] string changedPassword,
                                                [FromForm] string changedSurname,
                                                [FromForm] string changedName,
                                                [FromForm] string changedPhone,
                                                [FromForm] DateTime changedBirthaday,
                                                [FromForm] UserSex changedSex,
                                                [FromForm] UserRole changedRole)
        {
            await _repository.ChangeUserData(   UserId,
                                                changedEmail,
                                                changedPassword,
                                                changedSurname,
                                                changedName,
                                                changedPhone,
                                                changedBirthaday,
                                                changedSex,
                                                changedRole);

            return Ok();
        }

        [HttpDelete("{UserId}")]
        [Authorize]
        public async Task<OkResult> DeleteData(int UserId)
        {
            await _repository.RemoveUserData(UserId);

            return Ok();
        }

        [HttpGet("authenticate")]
        [AllowAnonymous]
        public async Task<object> LoginUser([FromQuery] string email,
                                                [FromQuery] string password)
        {            
            var user = await _repository.LoginUser(email, password);

            if (user == null)
            {
                return Unauthorized();
            }

            var id = user.Id.ToString();
            var name = user.Name.ToString();
            var role = user.Role.ToString();

            var token = Generate(id, name, role);

            return token;
        }
    }
}
