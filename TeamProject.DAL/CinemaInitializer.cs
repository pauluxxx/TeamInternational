using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject.DAL
{
   public class CinemaInitializer : DbMigrationsConfiguration<CinemaContext>,IDatabaseInitializer<CinemaContext>
    {
        public void Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        public void InitializeDatabase(CinemaContext context)
        {
            
        }

        protected override void Seed(CinemaContext context)
        {
        
    }
    }
}
