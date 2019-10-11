using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace Interfaces
{
    public interface IUserManager
    {
        Response<List<User>> GetAllUsers();
    }
}
