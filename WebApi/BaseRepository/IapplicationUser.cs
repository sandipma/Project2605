using BaseModels.ApplicationUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRepository
{
    public interface IapplicationUser
    {
        public Task<ApplicationUserDt> RegisterUser(ApplicationUserInsert appInsert);

        public Task<ApplicationUserDt> ValidateUser(ApplicationUserInsert appInsert);

        
    }
}
