using System.ComponentModel.DataAnnotations;

namespace ChatApp.Model
{
    public class Registration
    {
        [Key]
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
