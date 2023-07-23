using Azure.Core;
using BaseDataAccess;
using BaseModels.ApplicationUser;
using BaseModels.Employee;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BaseRepository
{
    public class Employee : IEmployee
    {
        private readonly DbContextClass _dbContext;
        private readonly IConfiguration _config;
        private IHostingEnvironment _hostingEnvironment;

        public Employee(DbContextClass dbContext, IConfiguration config, IHostingEnvironment environment)
        {
            _dbContext = dbContext;
            _config = config;
            _hostingEnvironment = environment ?? throw new ArgumentNullException(nameof(environment)); ;
        }
        public async Task<EmplyeeData> AddEmployeeAsync(EmployeeInsert empInsert)
        {
            EmplyeeData? employeeData = null;
            try
            {


                var parameter = new List<SqlParameter>();

                if (empInsert.EmployeeId == null)
                {
                    parameter.Add(new SqlParameter("@EmployeeId", System.Data.SqlDbType.Int) { Value = DBNull.Value });

                }
                parameter.Add(new SqlParameter("@EmployeeName", empInsert.EmployeeName));
                parameter.Add(new SqlParameter("@EmployeeCity", empInsert.EmployeeCity));
                parameter.Add(new SqlParameter("@EmpPosition", empInsert.EmpPosition));

                if (string.IsNullOrWhiteSpace(_hostingEnvironment.WebRootPath))
                {
                    _hostingEnvironment.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                }
                var path = Path.Combine(_hostingEnvironment.WebRootPath, "images/");

                //checking if "images" folder exist or not exist then create it
                if ((!Directory.Exists(path)))
                {
                    Directory.CreateDirectory(path);
                }
                //getting file name and combine with path and save it
                string filename = empInsert.ProfileImage.FileName;
                using (var fileStream = new FileStream(Path.Combine(path, filename), FileMode.Create))
                {
                    await empInsert.ProfileImage.CopyToAsync(fileStream);
                }
                parameter.Add(new SqlParameter("@EmpPhoto", "images/" + filename));
                parameter.Add(new SqlParameter("@ApplicationUserId", empInsert.ApplicationUserId));

                parameter.Add(new SqlParameter("@AppId", SqlDbType.Int) { Direction = ParameterDirection.Output });
                int? id = 0;
                _dbContext.Database.CreateExecutionStrategy();
                await Task.Run(() => _dbContext.Database
                .ExecuteSqlRawAsync(@"exec ApplicationUser_Upsert @EmployeeId, @EmployeeName, @EmployeeCity,@EmpPosition,@EmpPhoto,@ApplicationUserId,@AppId OUTPUT", parameter.ToArray()));
                id = Convert.ToInt32(parameter[6].Value);
                id = id ?? empInsert.EmployeeId;
                IEnumerable<EmplyeeData> applicationUser = await GetEmplyeeByIdAsync(id.Value);

                employeeData = (from n in applicationUser
                                where n.EmployeeId == id
                                select n).FirstOrDefault();

            }
            catch (Exception ex)
            {
                throw;
            }
            return employeeData;
        }

        public async Task<int> DeleteEmployeeAsync(int employeeId)
        {
            try
            {
                _dbContext.Database.CreateExecutionStrategy();
                return await Task.Run(() => _dbContext.Database.ExecuteSqlInterpolatedAsync($"DeleteEmployeebyId {employeeId}"));

            }
            catch (Exception ex)
            {
                throw new Exception("Preblem while deleteing employee" + ex);

            }
        }

        public async Task<List<EmplyeeData>> GetEmployeeListAsync(int applicationUserId)
        {
            List<EmplyeeData> employees = new List<EmplyeeData>();
            try
            {

                var param = new SqlParameter("@ApplicationUserId", applicationUserId);

               
                 _dbContext.Database.CreateExecutionStrategy();
                employees = await Task.Run(() => _dbContext.EmplyoeeData
                               .FromSqlRaw(@"exec GetAllEmployee @ApplicationUserId", param).ToListAsync());


            }
            catch (Exception ex)
            {

                throw new Exception("Unable to display employee list" + ex);
            }

            return employees;
        }

        public async Task<IEnumerable<EmplyeeData>> GetEmplyeeByIdAsync(int employeeId)
        {
            IEnumerable<EmplyeeData> empDetails = new List<EmplyeeData>();
            SqlParameter param = new SqlParameter("@EmployeeId", employeeId);
            try
            {
                _dbContext.Database.CreateExecutionStrategy();
                empDetails = await Task.Run(() => _dbContext.EmplyoeeData
                               .FromSqlRaw(@"exec GetAllEmployeebyId @EmployeeId", param).ToListAsync());
            }
            catch (Exception ex)
            {
                throw new Exception("unable to get employee details" + ex);

            }
            return empDetails;
        }

        public async Task<EmplyeeData> UpdateEmployeeAsync(EmployeeInsert empInsert)
        {
            EmplyeeData? employeeData = null;
            try
            {
                var parameter = new List<SqlParameter>();
                parameter.Add(new SqlParameter("@EmployeeId ", empInsert.EmployeeId));
                parameter.Add(new SqlParameter("@EmployeeName", empInsert.EmployeeName));
                parameter.Add(new SqlParameter("@EmployeeCity", empInsert.EmployeeCity));
                parameter.Add(new SqlParameter("@EmpPosition", empInsert.EmpPosition));
                var path = Path.Combine(_hostingEnvironment.WebRootPath, "images/");
                string filename = empInsert.ProfileImage.FileName;
                if (File.Exists(Path.Combine(path, filename)))
                {

                    parameter.Add(new SqlParameter("@EmpPhoto", "images/" + filename));
                }
                else

                {
                    System.IO.File.Delete(filename);
                    using var fileStream = new FileStream(Path.Combine(path, filename), FileMode.Create);
                    await empInsert.ProfileImage.CopyToAsync(fileStream);
                    parameter.Add(new SqlParameter("@EmpPhoto", "images/" + filename));
                }
                parameter.Add(new SqlParameter("@ApplicationUserId", empInsert.ApplicationUserId));
                parameter.Add(new SqlParameter("@AppId", SqlDbType.Int) { Direction = ParameterDirection.Output });
                int? id = 0;
                _dbContext.Database.CreateExecutionStrategy();
                await Task.Run(() => _dbContext.Database
                .ExecuteSqlRawAsync(@"exec ApplicationUser_Upsert @EmployeeId, @EmployeeName, @EmployeeCity,@EmpPosition,@EmpPhoto,@ApplicationUserId,@AppId OUTPUT", parameter.ToArray()));
                if (parameter[6].Value == DBNull.Value)
                {
                    id = null;
                    id = id ?? empInsert.EmployeeId;
                }

                IEnumerable<EmplyeeData> applicationUser = await GetEmplyeeByIdAsync(id.Value);

                employeeData = (from n in applicationUser
                                where n.EmployeeId == id
                                select n).FirstOrDefault();

            }
            catch (Exception ex)
            {
                throw;
            }
            return employeeData;
        }


    }
}
