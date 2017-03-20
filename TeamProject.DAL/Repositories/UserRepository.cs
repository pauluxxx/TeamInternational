using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamProject.DAL.Entities;
using TeamProject.DAL.Repositories.Interfaces;

namespace TeamProject.DAL.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private CinemaContext db;

        // Maybe we should create new CinemaContext();
        public UserRepository(CinemaContext db)
        {
            this.db = db;
        }

        public void Create(User user)
            => db.Users.Add(user);

        public void Delete(User user)
            => db.Users.Remove(user);

        public IEnumerable<User> GetAll()
            => db.Users.ToList();

        public User Get(int id)
            => db.Users.SingleOrDefault(user => user.ID == id);

        public void Update(User user)
            => db.Entry<User>(user).State = EntityState.Modified;
        public User GetByEmail(string mail)
            => db.Users.SingleOrDefault(user => user.Email == mail);

        public User GetByEmailAndPassword(string mail, string password)
        => db.Users.SingleOrDefault(user=>user.Email==mail&&user.Password==password);
    }
}
