using ChatApp.Model;
using System.ComponentModel.DataAnnotations;

namespace ChatApp.Models
{
    public class ReceiverInfo
    {
        [Key]
        public Guid ReceiverId { get; set; }
        public string Content { get; set; }
        public ICollection<MessageInfo> Messages { get; set; }
    }
}
