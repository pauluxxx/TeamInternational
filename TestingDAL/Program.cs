using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamProject.DAL;
using TeamProject.DAL.Entities;

namespace TestingDAL
{
    class Program
    {
        // Testing of creating, deleting and updating users.
        static void Main(string[] args)
        {
            var unit = new UnitOfWork();

            User newUser1 = new User() { ID = 1, Name = "Paul Kokorin" };
            User newUser2 = new User() { ID = 2, Name = "Paul Poymanov" };


            unit.Users.Create(newUser1);
            unit.Users.Create(newUser2);

            unit.Save();

            var users = unit.Users.GetAll();
            foreach (var user in users)
            {
                Console.WriteLine(user.Name);
                user.Name += " (Edited)";
                unit.Users.Update(user);
            }

            Console.WriteLine();
            unit.Save();
            Console.WriteLine();

            users = unit.Users.GetAll();
            foreach (var user in users)
            {
                Console.WriteLine(user.Name);
                unit.Users.Delete(user);
            }
            unit.Save();

        }
    }
}
