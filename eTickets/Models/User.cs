using MessagePack;
using System.ComponentModel.DataAnnotations;

namespace eTickets.Models
{


    public class User
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int Id { get; set; }
        //public string Username { get; set; } = string.Empty;
        //public string PasswordHash { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;

        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime TokenCreated { get; set; }
        public DateTime TokenExpires { get; set; }

        public string Role { get; set; } = string.Empty;



    }
}
