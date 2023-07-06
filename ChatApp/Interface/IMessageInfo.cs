using ChatApp.Model;

namespace ChatApp.Interface
{
    public interface IMessageInfo
    {
        public ICollection<MessageInfo> ConversationHistory();
        public void AddMessage(MessageInfo messageInfo);
        public void RemoveMessage(Guid msgId);    
        public void UpdateMessage(MessageInfo messageInfo);


    }
}
