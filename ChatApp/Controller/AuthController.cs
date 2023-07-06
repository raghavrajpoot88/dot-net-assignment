//using ChatApp.Helper;
//using ChatApp.Model;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using System.Security.Cryptography;

//namespace ChatApp.Controller
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class AuthController : ControllerBase
//    {
//        public static User user = new User();
        
//        [HttpPost("register")]
//        public async Task<ActionResult<User>> Register(Registration registered)
//        {
//            CreatePasswordHash(registered.Password, out byte[] passwordHash, out byte[] passwordSalt);

//            user.Email = registered.Email;
//            user.PasswordHash = passwordHash;
//            user.PasswordSalt = passwordSalt;

//            return Ok(user);
//        }
//        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
//        {
//            using (var hmac = new HMACSHA512())
//            {
//                passwordSalt = hmac.Key;
//                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
//            }
//        }
//    }
//}
