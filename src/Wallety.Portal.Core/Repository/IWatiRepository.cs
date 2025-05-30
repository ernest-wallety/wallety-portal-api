using Wallety.Portal.Core.Entity.Wati;
using Wallety.Portal.Core.Specs;

namespace Wallety.Portal.Core.Repository
{
    public interface IWatiRepository
    {
        Task<DataList<WatiTemplateEntity>> GetTemplates();

        Task<WatiTemplateEntity> GetTemplate(string code);
    }
}
