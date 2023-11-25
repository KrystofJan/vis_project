using System;
using System.Collections.ObjectModel;
using Generator;
using Models;
using ORM;

namespace GenerateData
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Address a = AddressDAO.SelectById(1);
            Console.WriteLine(a.country);
        }
    }
}