namespace Wallety.Portal.Application.Response
{
    public class LookupResponse
    {
        public object? Id { get; set; }
        public string Name { get; set; }
        public string? IdList { get; set; }
        public int[]? IdArr { get; set; }
        public string Code { get; set; }
        public string Colour { get; set; }
        public string ForeColour { get; set; }

        public string SelectedName { get; set; }
        public string Description { get; set; }
        public string GroupBy { get; set; }
        public bool IsActive { get; set; }
        public short? SortOrder { get; set; }
        public string Icon { get; set; }
        public bool? AltBoolValue { get; set; }
        public bool IsDefault { get; set; }
        public string PrimaryKey { get; set; }
    }
}
