using System;

namespace CodeSectorCMS.Domain.Managers.Interfaces
{
    public interface ITemplateManager : IDisposable  
    {
        List<Template> GetAllTemplate(int ClientId);
        void CreateNewTemplate(Template template);
        IQueryable<Template> GetTemplateByID(int ClientId, int TemplateID);
        void DeleteTemplateByID(int ClientId,  int TemplateID);
        void UpdateTemplate(Template template);
        void SaveChanges();
      
    }
  

}
