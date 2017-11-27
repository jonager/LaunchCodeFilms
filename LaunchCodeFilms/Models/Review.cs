using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LaunchCodeFilms.Models
{
    public class Review
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        public DateTime ReviewDate { get; set; } = DateTime.UtcNow;

        public int MovieId{ get; set; }// FK
        public Movie Movie { get; set; }

        public int UserId { get; set; } //FK
        public ApplicationUser User { get; set; }
    }
}
