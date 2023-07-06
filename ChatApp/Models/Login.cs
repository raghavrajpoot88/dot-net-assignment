using System.ComponentModel.DataAnnotations;

namespace ChatApp.Model
{
    public class Login
    {
        [Key]
        public Guid LoginId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Token { get; set; }
        //public ICollection<MessageInfo> MessageInfos { get; set; }
    }
}
