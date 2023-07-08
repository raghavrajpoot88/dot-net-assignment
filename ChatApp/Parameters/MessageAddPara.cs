using System.ComponentModel.DataAnnotations;

namespace ChatApp.Parameters
{
    public class MessageAddPara
    {
        [Required]
        public Guid ReceiverId { get; set; } = Guid.Empty;
        [Required]
        public string Content { get; set; }
    }
}
