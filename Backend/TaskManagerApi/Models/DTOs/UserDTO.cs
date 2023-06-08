using System.Text.Json.Serialization;

namespace TaskManagerApi.Models.DTOs;

public class UserDTO
{
    public long userID { get; set; }
    public string Username { get; set; }
    [JsonIgnore]
    public string Password { get; set; }
}