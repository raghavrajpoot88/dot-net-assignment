using ChatApp.Controller;
using ChatApp.Data;
using ChatApp.Interface;
using ChatApp.Model;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Repositories
{
    public class MessagesRepository : IMessages
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public MessagesRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        
        public async Task<ICollection<Messages>> GetMessage()
        {
            var result = await _applicationDbContext.messages.ToListAsync();
            return result;
        }
        public async Task<ICollection<Messages>> GetUser(Guid UserId)
            
        {

            var result =await _applicationDbContext.messages.
               Where(a => a.UserId == UserId).ToListAsync();

            return result;
        }

        public async Task<Messages> GetMessageById(Guid id)
        {

            var result = await _applicationDbContext.messages.
               Where(a => a.MsgId == id).FirstOrDefaultAsync();

            return result;
        }
        public async Task<ICollection<Messages>>GetConversationHistory(Guid UserId,Guid senderId, DateTime? before)
        {
            var MessageHistory=await _applicationDbContext.messages.Where(u => (u.ReceiverId==UserId && u.UserId==senderId
                ||(u.UserId == UserId && u.ReceiverId == senderId)) && (before == null || u.TimeStamp < before))
                .OrderBy(m => m.TimeStamp).ToListAsync();

            return MessageHistory;
        }
        
        public async Task AddMessage(Messages messageInfo)
        {
            var result= await _applicationDbContext.messages.AddAsync(messageInfo);
            await _applicationDbContext.SaveChangesAsync();
            
        }
        public async Task<Messages> UpdateMessage( Messages messageInfo)
        {
            var user = await _applicationDbContext.messages.FirstOrDefaultAsync(a=> a.MsgId==messageInfo.MsgId);
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
        public async Task RemoveMessage(Guid MsgId)
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
