namespace TaskManagerApi.Models.DTOs;

public class TaskDTO
{
    public long userTaskID { get; set; }
    public string TaskName { get; set; }
    public string TaskDescription { get; set; }
}