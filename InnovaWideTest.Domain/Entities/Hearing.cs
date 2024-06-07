using InnovaWideTest.Domain.Helper;

namespace InnovaWideTest.Domain.Entities
{
    public class Hearing : IMustHaveTenant
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Decision { get; set; }
        public string TenantId { get; set; }
        public int CaseId { get; set; }
        public Case Case { get; set; }

        public int LawyerId { get; set; }
        public Lawyer Lawyer { get; set; }

    }

}
