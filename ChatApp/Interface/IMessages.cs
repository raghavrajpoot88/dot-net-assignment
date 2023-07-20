using ChatApp.Model;

namespace ChatApp.Interface
{
    public interface IMessages
    {
        public Task<ICollection<Messages>> GetMessage();
        Task<Messages> GetMessageById(Guid id); 
        public Task<ICollection<Messages>> GetUser(Guid id);
        public Task<ICollection<Messages>> GetConversationHistory(Guid UserId , Guid userId,DateTime? before);
        public Task AddMessage(Messages messageInfo);
        public Task RemoveMessage(Guid MsgId);    
        public Task<Messages> UpdateMessage(Messages messageInfo);


    }
}
