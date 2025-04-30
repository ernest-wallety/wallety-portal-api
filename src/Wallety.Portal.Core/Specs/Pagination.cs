namespace Wallety.Portal.Core.Specs
{
    public class Pagination<T> where T : class
    {
        public Pagination(int pageIndex, int pageSize, int count, IReadOnlyList<T> items)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Count = count;
            Items = items;
        }

        public Pagination()
        {
            Items = [];
        }

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public long Count { get; set; }
        public IReadOnlyList<T> Items { get; set; }
    }
}
