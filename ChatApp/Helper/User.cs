namespace ChatApp.Helper
{
    public class User
    {
        public string Email { get; set; }= string.Empty;
        public string Name { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
