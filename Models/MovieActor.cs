using System.Collections.ObjectModel;

namespace Models;

public class MovieActor
{
    public Collection<Movie> Movies { get; set; }
    public Collection<Actor> Actors { get; set; }
}
