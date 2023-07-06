using ChatApp.Data;
using ChatApp.Interface;
using ChatApp.Model;

namespace ChatApp.Repositories
{
    public class RegistrationRepository : IRegistration
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public RegistrationRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public ICollection<Registration> GetUsers()
        {
            return _applicationDbContext.registrations.OrderBy(u => u.UserId).ToList();
        }
        public Registration GetUserById(Guid id)
        {
            return _applicationDbContext.registrations.Where(u => u.UserId == id).FirstOrDefault();
        }

        public void AddUser(Registration registeredUser)
        {
            _applicationDbContext.registrations.Add(registeredUser);
            _applicationDbContext.SaveChanges();
        }


    }
}
