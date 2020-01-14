using System;

namespace AttendanceServer.Entities
{
    public  class Attedance
    {
        int attedanceid, userId;
        DateTime checkIn;

        public int AttedanceId { get => attedanceid; set => attedanceid = value; }
        public int UserId { get => userId; set => userId = value; }
        public DateTime CheckIn { get => checkIn; set => checkIn = value; }
    }
}