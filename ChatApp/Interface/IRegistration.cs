using ChatApp.Model;
using System.Text.RegularExpressions;

namespace ChatApp.Interface
{
    public interface IRegistration
    {
        public ICollection<Registration> GetUsers();

        public Registration GetUserById(Guid id);

        public void AddUser(Registration registeredUser);
    }
}
