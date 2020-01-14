namespace AttendanceServer.Entities
{
    public class Department
    {

        int id;
        string name;

        public int DepartmentId { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
    }
}