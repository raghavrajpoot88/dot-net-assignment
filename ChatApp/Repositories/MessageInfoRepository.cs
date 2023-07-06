using ChatApp.Data;
using ChatApp.Interface;
using ChatApp.Model;

namespace ChatApp.Repositories
{
    public class MessageInfoRepository : IMessageInfo
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public MessageInfoRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public ICollection<MessageInfo> ConversationHistory()
        {
            throw new NotImplementedException();
        }

        public void RemoveMessage(Guid msgId)
        {
            throw new NotImplementedException();
        }
        void IMessageInfo.AddMessage(MessageInfo messageInfo)
        {
            _applicationDbContext.messages.Add(messageInfo);
            _applicationDbContext.SaveChanges();
        }

        void IMessageInfo.UpdateMessage(MessageInfo messageInfo)
        {
            throw new NotImplementedException();
        }
    }
}
