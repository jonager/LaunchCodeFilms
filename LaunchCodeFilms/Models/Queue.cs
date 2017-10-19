using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaunchCodeFilms.Models
{
    public class Queue
    {
        public int ID { get; set; }
        public bool Watched { get; set; }
        public bool Favorite { get; set; }
        public bool Watchlist { get; set; }
        public bool NotifyTheater { get; set; }

        public int MovieId { get; set; } // FK
        public Movie Movie { get; set; }

        public int UserId { get; set; } //FK
        public ApplicationUser User { get; set; }
    }
}
