using System.ComponentModel.DataAnnotations;

namespace projects.Data{
    public class Film{
        [Key]
        public int FilmId { get; set; }
        public string FilmName { get; set; }
        public string FilmType { get; set; }
        public double Imdb { get; set; }
    }
}