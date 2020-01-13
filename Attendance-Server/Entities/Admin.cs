using System;
using System.Collections.Generic;
using System.Text;

namespace AttendanceServer.Entities

{
    public class Admin
    {
        int id;
        string username, password;

        public int Id { get => id; set => id = value; }
        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        public string Token { get; internal set; }
    }
}
