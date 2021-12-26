using System;
using System.Net.Http;
using System.Text;

namespace ProjectRepository.Models
{
    public class Tasks
    {
        public int ID { get; set; }
        public int ProjectID { get; set; }
        public int Status { get; set; }
        public int AssiignedToUserID { get; set; }
        public string Detail { get; set; }
        public DateTime CreatedOn { get; set; }

        public static explicit operator HttpContent(Tasks v)
        {
            return new StringContent(System.Text.Json.JsonSerializer.Serialize(v), Encoding.UTF8, "application/json");
        }
    }
}
