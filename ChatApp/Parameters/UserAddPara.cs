using System.ComponentModel.DataAnnotations;

namespace ChatApp.ParameterModels
{
    public class UserAddPara
    {

        public string Email { get; set; }
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
