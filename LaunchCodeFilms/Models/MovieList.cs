using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaunchCodeFilms.Models
{
    public class MovieList
    {
        public int ID { get; set; }

        public int MovieId { get; set; }// FK
        public Movie Movie { get; set; }

        public int UserId { get; set; } //FK
        public ApplicationUser User { get; set; }

        public bool HasWatched { get; set; }
        public bool Watchlist{ get; set; }
        public bool HasRated { get; set; }
        public bool IsFavorite { get; set; }
        public bool HasReviewed { get; set; }
    }
}
