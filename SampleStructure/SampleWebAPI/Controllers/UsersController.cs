using BusinessEntities;
using BusinessEntityManager;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ViewModel;

namespace SampleWebAPI.Controllers
{
    public class UsersController : ApiController
    {
        private IUserManager _userManager;

        public UsersController(IUserManager userManager)
        {
            _userManager = userManager;
        }
       
        public Response<List<User>> Get()
        {
            var response = new Response<List<User>>();
            response = _userManager.GetAllUsers();
            return response;
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
