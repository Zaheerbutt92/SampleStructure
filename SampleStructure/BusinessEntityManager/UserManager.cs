using BusinessEntities;
using Interfaces;
using Logging;
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
        #region Properties
        private readonly IGenericUnitOfWork _user_repository;
        private readonly ILogger _logger;
        #endregion

        #region Constructor
        public UserManager(IGenericUnitOfWork userRepository, ILogger logger)
        {
            _user_repository = userRepository;
            _logger = logger;
        }
        #endregion

        Response<List<User>> IUserManager.GetAllUsers()
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
                _logger.Error(ex);
                response.StatusCode = 505;
                response.Message = "System Error";
            }
            return response;
        }
    }
}
