using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagerApi.Models;

public class User
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long userID { get; set; }
    
    public string Username { get; set; }
    public string Password { get; set; }
    public string Role { get; set; } = "User";
    public ICollection<UserTask?> UserTasks { get; set; }
}