using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pr14V.Models
{
    public class Movie
    {
        public int MovieID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Rating { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int AgeRating { get; set; }
        public string PosterPath { get; set; } 

        public int GenreID { get; set; }
        public virtual Genres Genre { get; set; } 
    }

}
