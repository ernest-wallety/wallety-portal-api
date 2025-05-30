namespace Wallety.Portal.Core.Entity.Wati
{
    public class WatiTemplatePayload
    {
        public List<WatiParameter> Parameters { get; set; }
        public string TemplateName { get; set; }
        public string BroadcastName { get; set; }
    }

    public class WatiParameter
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
