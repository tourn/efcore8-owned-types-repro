namespace Repro.Models
{
    public class StaffEntry<TTasks> where TTasks : new()
    {
        public TTasks Tasks { get; set; } = new();
    }

    public class Staffing
    {
        public int Id { get; set; }
        public StaffEntry<DefaultTasks> Oven { get; set; } = new();
        // if we add another entry here, the problem disappears
        // public StaffEntry<DefaultTasks> Bakery { get; set; } = new();
    }

    public class DefaultTasks
    {
        public double Operation { get; set; }
        public double Maintenance { get; set; }
    }
}