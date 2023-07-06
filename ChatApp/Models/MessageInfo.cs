using ChatApp.Models;
using System.ComponentModel.DataAnnotations;

namespace ChatApp.Model
{
    public class MessageInfo
    {
        [Key]
        public Guid MsgId { get; set; }
        public string LoginId{ get; set; } //Foreign Key
        public string ReceiverId{ get; set; } //Foreign Key
        public string MsgBody { get; set; }
        public DateTime TimeStamp { get; set; }
        public Login login{ get; set; }
        public ReceiverInfo Receiver { get; set;}

    }
}
