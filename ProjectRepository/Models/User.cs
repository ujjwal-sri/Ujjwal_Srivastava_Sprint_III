using System;
using System.Net.Http;
using System.Text;

namespace ProjectRepository.Models
{
    public class User
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public static explicit operator HttpContent(User v)
        {
            return new StringContent(System.Text.Json.JsonSerializer.Serialize(v), Encoding.UTF8, "application/json");
        }
    }

}
