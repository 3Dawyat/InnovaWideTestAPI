using InnovaWideTest.Domain.Helper;

namespace InnovaWideTest.Domain.Entities
{
    public class Case : IMustHaveTenant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }
        public int Year { get; set; }
        public string LitigationDegree { get; set; }
        public string FinalVerdict { get; set; }
        public string TenantId { get; set; }
        public List<Hearing> Hearings { get; set; } = new List<Hearing>();
    }

}
