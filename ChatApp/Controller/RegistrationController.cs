﻿using ChatApp.Data;
using ChatApp.Interface;
using ChatApp.Model;
using ChatApp.ParameterModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace ChatApp.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IRegistration _registration;
        private readonly IConfiguration _configuration;
        //private readonly ILogin _login;
        private readonly ApplicationDbContext _dbcontext;

        public RegistrationController(IRegistration registration, IConfiguration configuration,
                                        ApplicationDbContext applicationDbContext)
        {
            _registration = registration;
            _configuration = configuration;
            //_login = login;
            _dbcontext = applicationDbContext;
        }
        public static Registration registeredUser = new Registration();
        public static Login loggedUser = new Login();    


        [HttpGet]
        public IActionResult GetUsers()
        {
            var Users = _registration.GetUsers();

            return Ok(Users);
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(Guid id)
        {
            var User = _registration.GetUserById(id);
            return Ok(User);
        }
        [HttpPost("register")]
        public ActionResult<Registration> PostUser(UserAddPara registered)
        {
            try
            {
                //Validations
                if (string.IsNullOrEmpty(registered.Email) || registered.Email == "string")
                    throw new Exception("Email Required");
                if (string.IsNullOrEmpty(registered.Name) || registered.Name == "string")
                    throw new Exception("User name Required");
                var CheckUserExists = (from user in _registration.GetUsers()
                                       where user.Email.Equals(registered.Email)
                                       select user).Count();
                if (CheckUserExists > 0)
                {
                    throw new Exception("User Already exist");
                }
                CreatePasswordHash(registered.Password, out byte[] passwordHash, out byte[] passwordSalt);

                registeredUser.Email = registered.Email;
                registeredUser.Name = registered.Name;
                registeredUser.PasswordHash = passwordHash;
                registeredUser.PasswordSalt = passwordSalt;


                _registration.AddUser(registeredUser);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Ok(registeredUser);
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login (string Email,string Password)
        {

            var user = await _dbcontext.registrations.FirstOrDefaultAsync(ul => ul.Email == Email);
            if ( user== null||user.Email != Email)
            {
                return BadRequest("User is not Registered");
            }
            if (!VerifyPass(Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("Wrong Password.");
            }
            //loggedUser.Email = login.Email;
            //loggedUser.Password=login.Password;

            //_login.AddUser(loggedUser);

            string token = CreateToken(registeredUser);
            return Ok(token);
        }
        //private string CreateToken(Registration registration)
        //{
        //    List<Claim> claims = new List<Claim>
        //    {
        //        new Claim(ClaimTypes.Email, registration.Email),
        //    };
        //    var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSetting:Token").Value));

        //    var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        //    var token=new JwtSecurityToken(
        //        claims:claims,
        //        expires:DateTime.Now.AddDays(2),
        //        signingCredentials:credentials);

        //    var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        //    return jwt;
        //}
        private string CreateToken(Registration user)
        {
            List<Claim> claims = new List<Claim>();
            if (!string.IsNullOrEmpty(user.Email))
            {
                claims.Add(new Claim(ClaimTypes.Email, user.Email));
            }
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
        private bool VerifyPass(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            if (password == null || passwordHash == null || passwordSalt == null)
            {
                // Handle the case where any of the parameters are null
                return false;
            }

            try
            {
                using (var hmac = new HMACSHA512(passwordSalt))
                {
                    var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                    return computedHash.SequenceEqual(passwordHash);
                }
            }
            catch (Exception)
            { 
                return false;
            }
        }



    }
}