namespace eTickets.Controllers
{
    internal class ApplicationUser
    {
        public string Username { get; internal set; }
        public byte[] PasswordHash { get; internal set; }
        public byte[] PasswordSalt { get; internal set; }
        public string RefreshToken { get; internal set; }
        public DateTime TokenCreated { get; internal set; }
        public DateTime TokenExpires { get; internal set; }
    }
}