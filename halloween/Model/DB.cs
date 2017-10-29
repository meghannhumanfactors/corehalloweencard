using Microsoft.EntityFrameworkCore;

namespace halloween.Model
{
    public class DB : DbContext
    {
        // HEY! ADD THESE CONTRUCTORS!
        public DB() { }
        public DB(DbContextOptions<DB> options) : base(options) { }

        // HEY! CREATE A DB FOR EACH EXISTING MODEL(S)
        //public DbSet<Greetings> Friends { get; set; }
        //public DbSet<Greetings> Frenemies { get; set; }
        //public DbSet<Greetings> Enemies { get; set; }
        public DbSet<Greetings> Greetings { get; set; }



    }
}