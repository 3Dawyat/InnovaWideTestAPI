namespace InnovaWideTest.Domain.DTOs
{
    public class HearingDto
    {
        public int? Id { get; set; }
        public int CaseId { get; set; }
        public int LawyerId { get; set; }
        public DateTime Date { get; set; }
        public string Decision { get; set; }
    }
    public class HearingListDto
    {
        public int? Id { get; set; }
        public string Case { get; set; }
        public string Lawyer { get; set; }
        public DateTime Date { get; set; }
        public string Decision { get; set; }
    }
}
