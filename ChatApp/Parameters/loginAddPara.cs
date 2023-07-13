using System.ComponentModel.DataAnnotations;

namespace ChatApp.Parameters
{
    public class loginAddPara
    {
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
