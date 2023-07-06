//using ChatApp.Data;
//using ChatApp.Interface;
//using ChatApp.Model;

//namespace ChatApp.Repositories
//{
//    public class LoginRepository : ILogin
//    {
//        private readonly ApplicationDbContext _applicationDbContext;

//        public LoginRepository(ApplicationDbContext applicationDbContext)
//        {
//            _applicationDbContext = applicationDbContext;
//        }
//        public ICollection<Login> GetUsers()
//        {
//            return _applicationDbContext.logins.OrderBy(u => u.LoginId).ToList();
//        }
//        public void AddUser(Login loggedUser)
//        {
//            _applicationDbContext.logins.Add(loggedUser);
//            _applicationDbContext.SaveChanges();
//        }

//    }
//}
