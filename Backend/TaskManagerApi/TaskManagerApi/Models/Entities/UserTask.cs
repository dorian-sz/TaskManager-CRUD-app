using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagerApi.Models;

public class UserTask
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long userTaskID { get; set; }
    public string TaskName { get; set; }
    public string TaskDescription { get; set; }
    public User User { get; set; }
}