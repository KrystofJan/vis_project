using Models;
using ORM;

namespace Generator
{
    public class ActorGenerate
    {
        public static Actor Generage()
        {
            Actor actor = new Actor();
            actor.first_name = Faker.Name.First();
            actor.last_name = Faker.Name.Last();
            ActorDAO.Insert(actor);
            return actor;
        }
    }
}