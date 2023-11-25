using ORM;
using Models;

namespace Generator
{
    public class StockGenerate
    {

        public static Stock Generate()
        {
            Stock stock = new Stock();
            stock.movie = MovieGenerate.Generate();
            stock.storage = StorageGenerate.Generate();
            stock.ammount = Faker.RandomNumber.Next(999);
            StockDAO.Insert(stock);
            return stock;
        }

        public static Stock GenerateForMovie(Movie m)
        {
            Stock stock = new Stock();
            stock.movie = m;
            stock.storage = StorageGenerate.Generate();
            stock.ammount = Faker.RandomNumber.Next(999);
            StockDAO.Insert(stock);
            return stock;
        }

        public static Stock GenerateForStorage(Storage storage)
        {
            Stock stock = new Stock();
            stock.movie = MovieGenerate.Generate();
            stock.storage = storage;
            stock.ammount = Faker.RandomNumber.Next(999);
            StockDAO.Insert(stock);
            return stock;
        }
    }
}