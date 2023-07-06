//using ChatApp.Interface;
//using ChatApp.Model;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace ChatApp.Controller
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class LoginController : ControllerBase
//    {
//        private readonly ILogin _login;

//        public LoginController(ILogin login)
//        {
//            _login = login;
//        }
//        [HttpPost]
//        public IActionResult AddUser(Login LoggedUser)
//        {
//            _login.AddUser(LoggedUser);

//            return Ok("Successfully created login user");
//        }
        
//    }
//}
