using ChatApp.Data;
using ChatApp.Interface;
using ChatApp.Models;

namespace ChatApp.Repositories
{
    public class UserRepository : IUser
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public UserRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public ICollection<User> GetUsers()
        {
            return _applicationDbContext.users.OrderBy(u => u.UserId).ToList();
        }
        public User GetUserById(Guid id)
        {
            return _applicationDbContext.users.Where(u => u.UserId == id).FirstOrDefault();
        }

        public void AddUser(User registeredUser)
        {
            _applicationDbContext.users.Add(registeredUser);
            _applicationDbContext.SaveChanges();
        }


    }
}
