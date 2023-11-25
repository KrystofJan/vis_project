using System;
using ORM;
using Models;

namespace Generator
{
    public class AddressGenerate
    {
        public static Address Generate()
        {
            Address address = new Address();
            address = new Address();
            address.country = Faker.Address.Country();
            address.street = Faker.Address.StreetName();
            address.city = Faker.Address.City();
            address.building_number = Convert.ToString(Faker.RandomNumber.Next(999)) + "/" +
                                      Convert.ToString(Faker.RandomNumber.Next(99));
            AddressDAO.Insert(address);
            return address;
        }
    }
}