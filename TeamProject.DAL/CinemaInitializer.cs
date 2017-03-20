using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject.DAL
{
    class CinemaInitializer : MigrateDatabaseToLatestVersion<CinemaContext, System.Configuration>
    {
        protected override void Seed(CinemaContext context)
        {
        
    }
    }
}
