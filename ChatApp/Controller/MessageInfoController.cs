using ChatApp.Interface;
using ChatApp.Model;
using ChatApp.Parameters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace ChatApp.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageInfoController : ControllerBase
    {
        private readonly IMessageInfo _messageInfo;

        public MessageInfoController(IMessageInfo messageInfo)
        {
            _messageInfo = messageInfo;
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

                // Create a new message object
                var message = new MessageInfo
                {
                    MsgId = Guid.NewGuid(),
                    //LoginId = GetSenderIdFromToken(),
                    //ReceiverId = request.ReceiverId,
                    MsgBody = messageInfo.Content,
                    TimeStamp = DateTime.UtcNow
                };

                // TODO: Perform additional operations such as saving the message to the database

                // Return the success response with the message details
                //var response = new SendMessageResponse
                //{
                //    MessageId = message.MessageId,
                //    SenderId = message.SenderId,
                //    ReceiverId = message.ReceiverId,
                //    Content = message.Content,
                //    Timestamp = message.Timestamp
                //};

                return Ok(message);


            }
            catch (Exception ex)
            {
                throw;
            }

           
        }
        [HttpPut]
        [Authorize]
        public async Task<ActionResult<MessageInfo>> UpdateMessage(Guid id,[FromBody]string content)
        {
            try
            {
               
                //if (id != messageInfo.MsgId)
                //{
                //    return BadRequest("ID Mismatched");
                //}
                MessageInfo User = await _messageInfo.GetMessage(id);
                if(User != null)
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

        private Guid GetSenderIdFromToken(AuthenticationHeaderValue authHeader)
        {
            // Extract the token from the authorization header
            string token = authHeader.Parameter;
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwtToken = tokenHandler.ReadJwtToken(token);

            // Extract the sender user ID from the claims
            var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
            if (userIdClaim != null && Guid.TryParse(userIdClaim.Value, out Guid userId))
            {
                return userId;
            }
            return Guid.Empty;
        }

    }
}
