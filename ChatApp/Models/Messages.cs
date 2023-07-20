using ChatApp.Models;
using System.ComponentModel.DataAnnotations;

namespace ChatApp.Model
{
    public class Messages
    {
        [Key]
        public Guid MsgId { get; set; }
        public Guid UserId { get; set; } //Foreign Key
        public Guid ReceiverId{ get; set; } //Foreign Key
        public string MsgBody { get; set; }
        public DateTime TimeStamp { get; set; }
        
         
        

    }
}
