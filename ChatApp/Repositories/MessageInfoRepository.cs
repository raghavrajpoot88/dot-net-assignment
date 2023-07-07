using ChatApp.Controller;
using ChatApp.Data;
using ChatApp.Helper;
using ChatApp.Interface;
using ChatApp.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Validations;

namespace ChatApp.Repositories
{
    public class MessageInfoRepository : IMessageInfo
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public MessageInfoRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        
        public async Task<ICollection<MessageInfo>> GetMessages()
        {
            var result = await _applicationDbContext.messages.ToListAsync();
            return result;
        }

        public async Task<MessageInfo> GetMessage(Guid id)
        {

            var result = await _applicationDbContext.messages.
               Where(a => a.MsgId == id).FirstOrDefaultAsync();

            return result;
        }
        public Task<ICollection<MessageInfo>>ConversationHistory()
        {
            throw new NotImplementedException();
        }
        
        public async Task<MessageInfo> AddMessage(MessageInfo messageInfo)
        {
            var result= await _applicationDbContext.messages.AddAsync(messageInfo);
            await _applicationDbContext.SaveChangesAsync();
            return result.Entity;
        }
        public async Task<MessageInfo> UpdateMessage( MessageInfo messageInfo)
        {
            var user = _applicationDbContext.messages.FirstOrDefault(a=> a.UserId==messageInfo.UserId);
            if(user != null)
            {
                user.MsgId = messageInfo.MsgId;
                user.UserId = messageInfo.UserId;
                user.ReceiverId = messageInfo.ReceiverId;
                user.MsgBody = messageInfo.MsgBody;
                user.TimeStamp = messageInfo.TimeStamp;

                await _applicationDbContext.SaveChangesAsync();
                return user;
            }
            return null;

        }
        public async void RemoveMessage(Guid MsgId)
        {
            var result = await _applicationDbContext.messages.Where(a=>a.MsgId == MsgId).FirstOrDefaultAsync();
            if (result != null)
            {
                _applicationDbContext.messages.Remove(result);
                await _applicationDbContext.SaveChangesAsync();

            }
            await _applicationDbContext.SaveChangesAsync();

        }

    }
}
