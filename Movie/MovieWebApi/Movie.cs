using System.ComponentModel.DataAnnotations;

namespace MovieWebApi
{
    public class Movie
    {

        [Key]
        public int MovieId { get; set; }

        [Required]
        [StringLength(maximumLength: 50,MinimumLength =1)]
        public string MovieName { get; set; }
        public string  Category { get; set; }

        public int Duration { get; set; }

        [Required]
        [StringLength(maximumLength: 50, MinimumLength = 1)]
        public string Director { get; set; }
    }
}
