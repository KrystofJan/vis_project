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
            Collection<Movie> a = MovieDAO.Select();
            foreach (Movie m in a)
            {
                Console.WriteLine(m.movie_name);
            }
        }
    }
}