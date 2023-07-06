using ChatApp.Model;
using ChatApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ChatApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :
            base(options)
        {

        }

        public DbSet<Login> logins { get; set; }
        public DbSet<MessageInfo> messages { get; set; }
        public DbSet<ReceiverInfo> receivers { get; set; }
        public DbSet<Registration> registrations { get; set; }
    }
}
