using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LaunchCodeFilms.Models
{
    public class Movie
    {
        public int ID { get; set; }
        public int MovieDBID { get; set; }
        public double UserRating { get; set; }
        public int NumberRatings { get; set; }
        public int NumberReviews { get; set; }
        public int NumberFavorites { get; set; }

        public IList<Review> Reviews { get; set; }

        public IList<MovieList> MovieLists { get; set; }
    }
}
