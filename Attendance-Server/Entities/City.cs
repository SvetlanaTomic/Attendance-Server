namespace AttendanceServer.Entities
{
    public class City
    {
        int id;
        string name;

        public int CityId { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
    }
}