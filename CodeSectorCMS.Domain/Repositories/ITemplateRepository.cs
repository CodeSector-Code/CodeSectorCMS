using System;

namespace CodeSectorCMS.Domain.Repositories
{
    public interface ITemplateRepository : IDisposable
    {
        IEnumerable<Template> GetAllTemplates();
        Template GetTemplateByID(int TemplateID);
        IEnumerable<Template> FindByName(string name);
        IEnumerable<Template> FindBySubject(string subject);
        void InsertTemplate(Template template);
        void DeleteTemplate(int TemplateID);
        void UpdateTemplate(Template template);
        void Save();
    }
}
