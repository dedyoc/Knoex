using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace Knoex.Models
{
    public class User : IdentityUser<int>
    {
        [PersonalData]
        public string? FirstName { get; set; }
        [PersonalData]
        public string? LastName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int AcceptedAnswerCount { get; set; } = 0;
        public int QuestionCount { get; set; } = 0;
        public string Avatar(int size = 200)
        {
            using (var md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(Email.Trim().ToLowerInvariant());
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                var sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }

                return $"https://www.gravatar.com/avatar/{sb.ToString().ToLowerInvariant()}?d=identicon&s={size}";
            }
        }
    }
}