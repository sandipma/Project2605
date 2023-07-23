using BaseDataAccess;
using BaseModels.ApplicationUser;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BaseRepository
{
    public class ApplicationUser : IapplicationUser
    {
        private readonly DbContextClass _dbContext;
        private readonly IConfiguration _config;
        public ApplicationUser(DbContextClass dbContext, IConfiguration config)
        {
            _dbContext = dbContext;
            _config = config;
        }
        public async Task<ApplicationUserDt> RegisterUser(ApplicationUserInsert appInsert)
        {
            var parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@Username", appInsert.Username));
            parameter.Add(new SqlParameter("@UserPassword", appInsert.Userpassword));
            parameter.Add(new SqlParameter("@AppId", SqlDbType.Int) { Direction = ParameterDirection.Output });
            int? id = null;
            await Task.Run(() => _dbContext.Database
            .ExecuteSqlRawAsync(@"exec ApplicationUser_Insert @Username, @UserPassword,@AppId OUTPUT", parameter.ToArray()));
            IEnumerable<ApplicationUserDt> applicationUser = await GetByAllUserAsync();
            id = Convert.ToInt32(parameter[2].Value);
            var applicationUserData = (from n in applicationUser
                                       where n.ApplicationUserId == id
                                       select n).FirstOrDefault();

            return applicationUserData;
        }
        private async Task<IEnumerable<ApplicationUserDt>> GetByAllUserAsync()
        {

            return await _dbContext.UserData
                .FromSqlRaw<ApplicationUserDt>("GetApplicationUser")
                .ToListAsync();

        }

        public async Task<ApplicationUserDt> ValidateUser(ApplicationUserInsert appInsert)
        {
            string stringToken = string.Empty;
            IEnumerable<ApplicationUserDt> applicationUser = await GetByAllUserAsync();
            var applicationUserData =  applicationUser.
                Where(x => x.Username == appInsert.Username && x.UserPassword==appInsert.Userpassword).FirstOrDefault();
                                      

            if (applicationUserData.Username == appInsert.Username && applicationUserData.UserPassword == appInsert.Userpassword)
            {
                var issuer = _config["Jwt:Issuer"];
                var key = (Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                new Claim("Id", Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, appInsert.Username),
                new Claim("Password",appInsert.Userpassword),
                new Claim(JwtRegisteredClaimNames.Jti,
                Guid.NewGuid().ToString())
             }),
                    Expires = DateTime.UtcNow.AddMinutes(5),
                    Issuer = issuer,

                    SigningCredentials = new SigningCredentials
                    (new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha512Signature)
                };
                var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var jwtToken = tokenHandler.WriteToken(token);
                stringToken = tokenHandler.WriteToken(token);

            }
            ApplicationUserDt applicationUserDataFinal = new ApplicationUserDt
            {
                ApplicationUserId = applicationUserData.ApplicationUserId,
                Username = applicationUserData.Username,
                UserPassword = applicationUserData.UserPassword,
                Token = stringToken
            };

            return applicationUserDataFinal;
        }


    }
}
