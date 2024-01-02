using System.ComponentModel.DataAnnotations;

namespace projects.Data{
   public class List{
    [Key]
    public DateTime Date{get;set;}
    public string FilmName { get; set; }

    public string UserName { get; set; }
   } 
}