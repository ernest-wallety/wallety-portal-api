namespace Wallety.Portal.Application.Response
{
    public class WatiTemplateResponse
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string JsonPayload { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
