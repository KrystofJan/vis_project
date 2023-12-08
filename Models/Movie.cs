using System;
using System.Collections.ObjectModel;

namespace Models
{
    public class Movie
    {
        public int movie_id { get; set; }
        public String movie_name { get; set; }
        public decimal price_per_day { get; set; }
        public string picture_path { get; set; }
        public Collection<Actor> actors { get; set; }
    }
}