using System;
using System.Collections.Generic;
using System.Text;

namespace AttendanceServer.Entities
{
    public class User
    {
        int id;
        string username, password, name, lastname;
        Department department;
        City city;
        List<Attedance> attedances;
        public int Id { get => id; set => id = value; }
        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        public string Name { get => name; set => name = value; }
        public string Lastname { get => lastname; set => lastname = value; }
        public string Token { get; internal set; }
        internal Department Department { get => department; set => department = value; }
        internal City City { get => city; set => city = value; }
        internal List<Attedance> Attedances { get => attedances; set => attedances = value; }
    }
}
