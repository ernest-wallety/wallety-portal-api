namespace Wallety.Portal.Core.Specs
{
    public class DataList<T> where T : class
    {
        public DataList(int count, IReadOnlyList<T> items)
        {
            Count = count;
            Items = items;
        }

        public DataList()
        {
            Items = [];
        }

        public long Count { get; set; }
        public IReadOnlyList<T> Items { get; set; }
    }
}
