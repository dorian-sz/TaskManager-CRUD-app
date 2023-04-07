using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagerApi.Models;

public class UserTask
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long ID { get; set; }
    public string TaskName { get; set; }
    public string TaskDescription { get; set; }
    public List<User> Users { get; set; } = new List<User>();
}