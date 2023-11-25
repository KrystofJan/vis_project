using System;
using System.Collections.ObjectModel;
using ORM;
using Models;

namespace Generator
{
    public class MovieGenerate
    {
        public static Movie Generate()
        {
            Movie movie = new Movie();
            movie.movie_name = Convert.ToString(Faker.Lorem.Words(3));
            movie.price_per_day = Convert.ToDecimal(Faker.RandomNumber.Next(99));
            int actorCount = Faker.RandomNumber.Next(5);
            Collection<Actor> actors = new Collection<Actor>();
            for (int i = 0; i < actorCount; i++)
            {
                Actor tmp = ActorGenerate.Generage();
                actors.Add(tmp);
            }

            movie.actors = actors;
            MovieDAO.Insert(movie);

            return movie;

        }
    }
}