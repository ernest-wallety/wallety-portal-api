namespace Wallety.Portal.Core.Helpers
{
    public class ExcelExportData
    {
        public IEnumerable<object> Data { get; set; }
        public List<string> PropertyHeaders { get; set; }
        public List<string> ExcelHeaders { get; set; }
        public string SheetName { get; set; }
    }
}
