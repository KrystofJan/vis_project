using ORM;
using Models;

namespace Generator
{
    public class CustomerGenerate
    {
        public static Customer Generate()
        {
            Customer customer = new Customer();
            customer.address = AddressGenerate.Generate();
            customer.phone = Faker.Phone.Number();
            customer.first_name = Faker.Name.First();
            customer.last_name = Faker.Name.Last();
            customer.email = Faker.Internet.Email();
            customer.total_rentals = Faker.RandomNumber.Next(99);

            CustomerDAO.Insert(customer);
            return customer;
        }
    }
}