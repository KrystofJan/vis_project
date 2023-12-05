using System;
using ORM;
using Models;

namespace Generator
{

    public class EmployeeGenerate
    {
        public static Employee Generate()
        {
            Employee employee = new Employee();
            employee.address = AddressGenerate.Generate();
            employee.phone = Faker.Phone.Number();
            employee.first_name = Faker.Name.First();
            employee.last_name = Faker.Name.Last();
            employee.email = Faker.Internet.Email();
            employee.salary = Convert.ToDecimal(Faker.RandomNumber.Next(99999));
            employee.storage = StorageGenerate.Generate();

            EmployeeDAO.Insert(employee);

            return employee;
        }
    }
}