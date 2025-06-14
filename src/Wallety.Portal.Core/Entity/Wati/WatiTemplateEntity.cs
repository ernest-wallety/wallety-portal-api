namespace Wallety.Portal.Core.Entity.Wati
{
    public class WatiTemplateEntity
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string JsonPayload { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
