using AttendanceServer.Entities;
using AttendanceServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceServer.Data
{
    public class DbInitializer
    {
        public static void Initialize(AttendanceContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Users.Any() && context.Admins.Any() && context.Cities.Any() && context.Departments.Any() && context.Attedances.Any())
            {
                return;   // DB has been seeded
            }

            var users = new User[]
            {
                new User{ Name = "User1", Lastname = "User1", Username = "user1", Password = "user1"},
                new User{Name = "User2", Lastname = "User2", Username = "user2", Password = "user2"},
                new User{ Name = "User3", Lastname = "User3", Username = "user3", Password = "user3"}


            };
            foreach (User s in users)
            {
                context.Users.Add(s);
            }

                context.SaveChanges();
          
            var citys = new City[]
            {
                new City{Name = "Banja Luka"},
                new City{Name = "Beograd"},
                new City{Name = "Atina"},
                new City{Name = "Moskva"},

            };
            foreach (City c in citys)
            {
                context.Cities.Add(c);
            }
            context.SaveChanges();

            var attendances = new Attedance[]
            {
            new Attedance{ UserId = 1, CheckIn = new DateTime()},
            new Attedance{ UserId = 2, CheckIn = new DateTime()},
            new Attedance{ UserId = 3, CheckIn = new DateTime()},
            new Attedance{ UserId = 1, CheckIn = new DateTime()},
            new Attedance{ UserId = 1, CheckIn = new DateTime()}

            };
            foreach (Attedance e in attendances)
            {
                context.Attedances.Add(e);
            }
            context.SaveChanges();

            var admins = new Admin[]
            {
            new Admin{Username = "admin1", Password = "admin1"},

            };
            foreach (Admin e in admins)
            {
                context.Admins.Add(e);
            }
            context.SaveChanges();

            var departments = new Department[]
            {
                new Department{Name = "Management"},
                new Department{ Name = "Developers"},

            };
            foreach (Department e in departments)
            {
                context.Departments.Add(e);
            }
            context.SaveChanges();
        }
    
}
}
