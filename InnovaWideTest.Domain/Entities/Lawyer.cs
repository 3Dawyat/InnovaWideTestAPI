using InnovaWideTest.Domain.Helper;

namespace InnovaWideTest.Domain.Entities
{
    public class Lawyer : IMustHaveTenant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public string TenantId { get; set; }
        public List<Hearing> Hearings { get; set; } = new List<Hearing>();
    }

}
