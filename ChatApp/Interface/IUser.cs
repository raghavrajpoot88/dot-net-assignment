using ChatApp.Models;
using System.Text.RegularExpressions;

namespace ChatApp.Interface
{
    public interface IUser
    {
        public ICollection<User> GetUsers();

        public User GetUserById(Guid id);

        public void AddUser(User registeredUser);
    }
}
