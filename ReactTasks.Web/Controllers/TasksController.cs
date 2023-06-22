using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using ReactTasks.Data;
using ReactTasks.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ReactTasks.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {

        private string _connectionString;

        //public TasksController(IConfiguration configuration)
        //{
        //    _connectionString = configuration.GetConnectionString("ConStr");
        //}
        public TasksController( IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");

        }
        [HttpPost]
        [Route("adduser")]
        public void AddUser(User user)
        {
            var repo = new Repository(_connectionString);
            repo.AddUser(user);
        }
        //[HttpPost]
        //[Route("login")]
        //public User Login(LoginVM vm)
        //{
        //    var repo = new Repository(_connectionString);
        //    var user = repo.Login(vm.Email, vm.Password);
        //    if (user == null)
        //    {
        //        return null;
        //    }

        //    var claims = new List<Claim>
        //    {
        //        //When logging in a user, we put their email into the cookie
        //        //and then this will be available via User.Indetity.Name
        //        new Claim("user", vm.Email)
        //    };

        //    //The next line of code is the one that actually signs in the user
        //    //it basically sets a special cookie on the clients machine that
        //    //sets them as "logged in". Before using it though, make sure to add a
        //    //using on top:
        //    //using Microsoft.AspNetCore.Authentication;
        //    HttpContext.SignInAsync(new ClaimsPrincipal(
        //        new ClaimsIdentity(claims, "Cookies", "user", "role"))).Wait();

        //    return user;
        //}
        [HttpPost]
        [Route("login")]
        public User Login(LoginVM viewModel)
        {
            var repo = new Repository(_connectionString);
            var user = repo.Login(viewModel.Email, viewModel.Password);
            if (user == null)
            {
                return null;
            }
            var claims = new List<Claim>
            {
                new Claim("user", viewModel.Email)
            };
            HttpContext.SignInAsync(new ClaimsPrincipal(
                new ClaimsIdentity(claims, "Cookies", "user", "role"))).Wait();
            return user;
        }
        [HttpGet]
        [Route("getcurrentuser")]
        public User GetCurrentUser()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return null;
            }

            var repo = new Repository(_connectionString);
            return repo.GetByEmail(User.Identity.Name);
        }

        [HttpPost]
        [Route("logout")]
        public RedirectResult Logout()
        {
            HttpContext.SignOutAsync().Wait();
            return Redirect("/");
        }


    }
}
