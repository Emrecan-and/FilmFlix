using System.ComponentModel.DataAnnotations;

namespace projects.Data
{
    public class User{
     [Key]     
     public string UserName { get; set; }
     public string UserPassword { get; set; }
    }
}