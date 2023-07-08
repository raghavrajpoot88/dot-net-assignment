using ChatApp.Model;

namespace ChatApp.Interface
{
    public interface IMessageInfo
    {
        public Task<ICollection<MessageInfo>> GetMessages();
        Task<MessageInfo> GetMessage(Guid id); 
        public Task<ICollection<MessageInfo>> GetConversationHistory();
        public Task AddMessage(MessageInfo messageInfo);
        public Task RemoveMessage(Guid MsgId);    
        public Task<MessageInfo> UpdateMessage(MessageInfo messageInfo);


    }
}
