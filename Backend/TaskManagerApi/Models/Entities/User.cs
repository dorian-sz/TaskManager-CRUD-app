using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagerApi.Models;

public class User
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long ID { get; set; }
    
    public string UserName { get; set; }
    public string Password { get; set; }
}