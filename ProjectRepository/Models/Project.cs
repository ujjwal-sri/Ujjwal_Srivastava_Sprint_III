using System;
using System.Net.Http;
using System.Text;

namespace ProjectRepository.Models
{
    public class Project
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }
        public DateTime CreatedOn { get; set; }

        public static explicit operator HttpContent(Project v)
        {
            return new StringContent(System.Text.Json.JsonSerializer.Serialize(v), Encoding.UTF8, "application/json");
        }
    }
}
