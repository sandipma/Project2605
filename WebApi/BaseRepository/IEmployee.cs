using BaseModels.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRepository
{
    public interface IEmployee
    {
        public Task<List<EmplyeeData>> GetEmployeeListAsync(int applicationUserId);
        public Task<IEnumerable<EmplyeeData>> GetEmplyeeByIdAsync(int employeeId);
        public Task<EmplyeeData> AddEmployeeAsync(EmployeeInsert empInsert);
        public Task<EmplyeeData> UpdateEmployeeAsync(EmployeeInsert empInsert);
        public Task<int> DeleteEmployeeAsync(int employeeId);
    }
}
