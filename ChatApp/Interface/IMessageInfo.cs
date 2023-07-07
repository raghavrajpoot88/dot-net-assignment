using ChatApp.Model;

namespace ChatApp.Interface
{
    public interface IMessageInfo
    {
        public Task<ICollection<MessageInfo>> GetMessages();
        Task<MessageInfo> GetMessage(Guid id); 
        public Task<ICollection<MessageInfo>> ConversationHistory();
        public Task<MessageInfo> AddMessage(MessageInfo messageInfo);
        public void RemoveMessage(Guid MsgId);    
        public Task<MessageInfo> UpdateMessage(MessageInfo messageInfo);


    }
}
