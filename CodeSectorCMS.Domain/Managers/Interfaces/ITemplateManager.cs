using System;

namespace CodeSectorCMS.Domain.Managers.Interfaces
{
    public interface ITemplateManager : IDisposable  
    {
        List<Template> GetAllTemplate(int UserId);
        void CreateNewTemplate(Template template);
        IQueryable<Template> GetTemplateByID(int UserId, int TemplateID);
        void DeleteTemplateByID(int UserId,  int TemplateID);
        void UpdateTemplate(Template template);
        void SaveChanges();
      
    }
  

}
