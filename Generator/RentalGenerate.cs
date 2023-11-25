using System;
using System.Collections.ObjectModel;
using ORM;
using Models;

namespace Generator
{
    public class RentalGenerate
    {

        public static Rental Generate()
        {
            Rental rental = new Rental();
            rental.date_of_start = DateTime.Now;
            rental.date_of_return = DateTime.Now.AddDays(14);
            rental.customer = CustomerGenerate.Generate();
            rental.employee = EmployeeGenerate.Generate();
            rental.is_returned = false;

            RentalDAO.Insert(rental);

            int number_of_discs = Faker.RandomNumber.Next(5);
            Collection<Disc> discs = new Collection<Disc>();
            for (int i = 0; i < number_of_discs; i++)
            {
                Disc disc = DiscGenerate.GenerateForRental(rental);
                discs.Add(disc);
            }

            rental.discs = discs;

            return rental;
        }
    }
}