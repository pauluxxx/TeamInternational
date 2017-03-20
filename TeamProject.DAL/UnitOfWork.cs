using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamProject.DAL.Repositories;

namespace TeamProject.DAL
{

    public class UnitOfWork:ICinemaWork//to inject 
    {
        private CinemaContext db;

        private UserRepository userRepository;
        private MovieRepository movieRepository;
        private ViewRepository viewRepository;

        public UnitOfWork()
        {
            db = new CinemaContext();
        }

        public UserRepository Users
            => userRepository ?? (userRepository = new UserRepository(db));

        public MovieRepository Movies
            => movieRepository ?? (movieRepository = new MovieRepository(db));

        public ViewRepository Views
            => viewRepository ?? (viewRepository = new ViewRepository(db));

        public void Save()
            => db.SaveChanges();
    }
}
