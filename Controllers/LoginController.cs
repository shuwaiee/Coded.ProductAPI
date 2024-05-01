using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Models.Entites;
using ProductApi.Models.Requests;
using ProductApi.Models.Responses;
using ProductApi.Services;

namespace ProductApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly TokenService service;
        private readonly BankContext context;

        public LoginController(TokenService service, BankContext context)
        {
           this.service = service;
           this.context = context;
        }
        [HttpPost]
        public IActionResult Login(UserLoginRequest loginDetails)
        {
            //var newAccount =  UserAccount.Create();
            //var service = new TokenService(null,new BankContext("Data Source=db.app"))
            var response = service.GenerateToken(loginDetails.Username, loginDetails.Password);
            if (response.IsValid)
            {
                return Ok(new UserLoginResponse { Token = response.Token });
            }
            return BadRequest("Username and/or Password is wrong");
        }

        [HttpPost("Registor")]
        public IActionResult Registor(SignupRequest request)
        {
            //var isAdmin = false;
            //if (context.UserAccounts.Count() == 0)
            //{
            //    isAdmin = true;
            //}
            var newUser = UserAccountEntity.Create(request.Username, request.Password, request.IsAdmin);
            newUser.CivilId = request.CivilId;
            newUser.Email = request.Email;
            context.UserAccounts.Add(newUser);
            context.SaveChanges();

            return Ok(new {Message = "User Created"});
            
        }
    }
}
