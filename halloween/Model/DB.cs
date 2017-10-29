using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace halloween.Model
{
    public class DB : DbContext
    {
        //DEFAULT CONSTRUCTOR
        public DB() { }

        //CONSTRUCTOR
        public DB(DbContextOptions<DB> options): base(options) { }

        //CUSTOMIZED - CREATE A DB FOR EACH EXISTING MODEL(S)
        public DbSet<Greetings> Greetings { get; set; }



    }
}
