using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization; 

namespace Elipse.Models
{
    public class Chat
    {
        [JsonIgnore]
        public int Id { get; set; }

        public string? Text { get; set; }
    }
}
