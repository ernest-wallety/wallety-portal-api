namespace Wallety.Portal.Core.Requests.Common
{
    public class DateRangeModel
    {
        public string? Name { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsOnClear { get; set; }
        public bool IsOnChange { get; set; }
        public bool IsRange { get; set; }
    }
}
