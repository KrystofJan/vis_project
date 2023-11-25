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
            employee.person_detail = new PersonDetail();
            employee.person_detail.address = AddressGenerate.Generate();
            employee.person_detail.phone = Faker.Phone.Number();
            employee.person_detail.first_name = Faker.Name.First();
            employee.person_detail.last_name = Faker.Name.Last();
            employee.person_detail.email = Faker.Internet.Email();
            employee.salary = Convert.ToDecimal(Faker.RandomNumber.Next(99999));
            employee.storage = StorageGenerate.Generate();

            EmployeeDAO.Insert(employee);

            return employee;
        }
    }
}