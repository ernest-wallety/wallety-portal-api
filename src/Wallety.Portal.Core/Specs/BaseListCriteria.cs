namespace Wallety.Portal.Core.Specs
{
    public class BaseListCriteria
    {
        private const int MaxPageSize = 1000;

        public int PageIndex { get; set; } = 1;

        private int _pageSize = 100;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        public string? SortField { get; set; } = "1";
        public bool SortAscending { get; set; } = true;
        public string? Search { get; set; } = "";
        public DateTime? FromDate { get; set; } = null;
        public DateTime? ToDate { get; set; } = null;
        public string? Lookups { get; set; } = null;
        public string? Ranges { get; set; } = null;
        public int? Id { get; set; }
    }
}
