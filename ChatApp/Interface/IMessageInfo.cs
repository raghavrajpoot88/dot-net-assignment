using ChatApp.Model;

namespace ChatApp.Interface
{
    public interface IMessageInfo
    {
        public Task<ICollection<MessageInfo>> GetMessages();
        Task<MessageInfo> GetMessage(Guid id); 
        public Task<ICollection<MessageInfo>> GetUser(Guid id);
        public Task<ICollection<MessageInfo>> GetConversationHistory(Guid UserId , Guid userId,DateTime? before);
        public Task AddMessage(MessageInfo messageInfo);
        public Task RemoveMessage(Guid MsgId);    
        public Task<MessageInfo> UpdateMessage(MessageInfo messageInfo);


    }
}
