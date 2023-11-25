using ORM;
using Models;

namespace Generator
{
    public class CustomerGenerate
    {
        public static Customer Generate()
        {
            Customer customer = new Customer();
            customer.person_detail = new PersonDetail();
            customer.person_detail.address = AddressGenerate.Generate();
            customer.person_detail.phone = Faker.Phone.Number();
            customer.person_detail.first_name = Faker.Name.First();
            customer.person_detail.last_name = Faker.Name.Last();
            customer.person_detail.email = Faker.Internet.Email();
            customer.total_rentals = Faker.RandomNumber.Next(99);

            CustomerDAO.Insert(customer);
            return customer;
        }
    }
}