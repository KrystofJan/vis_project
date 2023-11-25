using ORM;
using Models;

namespace Generator
{
    public class DiscGenerate
    {
        public static Disc Generate()
        {
            Disc disc = new Disc();
            disc.rental = RentalGenerate.Generate();
            disc.movie = MovieGenerate.Generate();

            DiscDAO.Insert(disc);

            return disc;
        }

        public static Disc GenerateForMovie(Movie movie)
        {
            Disc disc = new Disc();
            disc.rental = RentalGenerate.Generate();
            disc.movie = movie;

            DiscDAO.Insert(disc);

            return disc;
        }

        public static Disc GenerateForRental(Rental rental)
        {
            Disc disc = new Disc();
            disc.rental = rental;
            disc.movie = MovieGenerate.Generate();

            DiscDAO.Insert(disc);

            return disc;
        }
    }
}