using System.Linq;

namespace Ex1Game.DataBase.Init
{
    public class DBLoader
    {

        private readonly PlayerDBContext dbContext;

        public DBLoader(PlayerDBContext context)
        {
            dbContext = context;
        }

        public void Load()
        {
            dbContext.Database.EnsureCreated();

            if (dbContext.Player.Any())
            {
                return;
            }

            dbContext.Player.Add(new Entity.Player());
            dbContext.Player.Add(new Entity.Player());
            dbContext.Player.Add(new Entity.Player());

            dbContext.SaveChanges();
        }
    }
}
