using BusinessEntities;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWork;
using ViewModel;

namespace BusinessEntityManager
{
    public class UserManager : IUserManager
    {
        private IGenericUnitOfWork _user_repository { get; }

        public UserManager(IGenericUnitOfWork userRepository)
        {
            _user_repository = userRepository;
        }

        public Response<List<User>> GetAllUsers()
        {
            var response = new Response<List<User>>();
            try
            {
                var users = _user_repository.User.GetAll().ToList();
                if(users != null )
                {
                    response.Data = users;
                    response.StatusCode = 200;
                    response.Message = "Success";
                }
                else
                {
                    response.StatusCode = 202;
                    response.Message = "No Data Avaliable";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = 505;
                response.Message = "System Error";
            }
            return response;
        }
    }
}
