using ChatApp.Data;
using ChatApp.Helper;
using ChatApp.Interface;
using ChatApp.Model;
using ChatApp.Parameters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using System.IdentityModel.Tokens.Jwt;
using System.IO.Compression;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;

namespace ChatApp.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageInfoController : ControllerBase
    {
        private readonly IMessageInfo _messageInfo;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;

        public MessageInfoController(IMessageInfo messageInfo, IConfiguration configuration,ApplicationDbContext applicationDbContext)
        {
            _messageInfo = messageInfo;
            _configuration = configuration;
            _context = applicationDbContext;
        }
        [HttpGet]
        [Authorize]
        public async Task<ICollection<MessageInfo>> ConversationHistory(Guid UserId, DateTime? before=null, int count=20,string sort="asc")
        {
            return await _messageInfo.GetConversationHistory(UserId);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<MessageInfo>> AddMessaage(MessageAddPara messageInfo)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                string currentUser = GetSenderIdFromToken();
                var senderId=await _context.registrations.FirstOrDefaultAsync(u => u.Email == currentUser);

                // Create a new message object
                var message = new MessageInfo
                {
                    MsgId = Guid.NewGuid(),
                    UserId = senderId.UserId,
                    ReceiverId = messageInfo.ReceiverId,
                    MsgBody = messageInfo.Content,
                    TimeStamp = DateTime.UtcNow
                };
                 _context.messages.Add(message);
                await _context.SaveChangesAsync();
                //await _messageInfo.AddMessage(message);
                var response = new MessageInfo
                {
                    MsgId = message.MsgId,
                    UserId = message.UserId,
                    ReceiverId = message.ReceiverId,
                    MsgBody = message.MsgBody,
                    TimeStamp = message.TimeStamp
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<MessageInfo>> UpdateMessage(Guid id,[FromBody]string content)
        {
            try
            {
                var currentUser= GetSenderIdFromToken();
                var SenderId= await _context.registrations.FirstOrDefaultAsync(u => u.Email == currentUser);
                MessageInfo User = await _messageInfo.GetMessage(id);
                //if ()
                //{
                //    return BadRequest("ID Mismatched");
                //}
                if (User.UserId != SenderId.UserId)
                {
                    return Unauthorized();
                }
                if(User == null|| id != User.MsgId)
                {
                    return NotFound($"User Id={id} not found ");
                }
                User.MsgBody=content;
                var UpdatedMsg = await _messageInfo.UpdateMessage(User);
                return UpdatedMsg;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> RemoveMsg(Guid id)
        {
            try
            {
                var currentUser= GetSenderIdFromToken();
                var SenderId = await _context.registrations.FirstOrDefaultAsync(u => u.Email == currentUser);
                MessageInfo User = await _messageInfo.GetMessage(id);
                if (User.UserId != SenderId.UserId)
                {
                    return Unauthorized();
                }
                if (User == null || id != User.MsgId)
                {
                    return NotFound($"User Id={id} not found ");
                }
                
                 await _messageInfo.RemoveMessage(id);
                return Ok("Successfully Deleted");

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private string GetSenderIdFromToken()
        {
            // Extract the token from the authorization header
            //var authHeader = Request.Headers["Authorization"].FirstOrDefault();
            //var token = authHeader?.Replace("Bearer", " ");

            //var tokenHandler = new JwtSecurityTokenHandler();

            //var tokenValidationParameters = new TokenValidationParameters
            //{
            //    ValidateIssuer = false, // Set to true if you want to validate the token issuer
            //    ValidateAudience = false, // Set to true if you want to validate the token audience
            //    ValidateIssuerSigningKey = true, // Set to true if you want to validate the token signing key
            //    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value)),
            //    ValidateLifetime = true // Set to true if you want to validate the token expiration time
                
            //};
            try
            {
                // Validate and decode the JWT token
                var claimsPrincipal = HttpContext.User;
                var userIdClaim = claimsPrincipal.FindFirst(ClaimTypes.Email);
                var emailClaim = claimsPrincipal.FindFirst(ClaimTypes.Email);

                return emailClaim?.Value;

                //if (userIdClaim == null)
                //{
                //    throw new Exception("User Id not found in token.");
                //}

                //if (!Guid.TryParse(userIdClaim.Value, out Guid userId))
                //{
                //    throw new Exception("Invalid user Id in token.");
                //}

                //return userId;
            }
            catch (SecurityTokenException ex)
            {
                // Handle token validation errors and exceptions according to your application's needs
                throw new Exception(
                    "Invalid token.", ex);
            }
        }

    }
}
