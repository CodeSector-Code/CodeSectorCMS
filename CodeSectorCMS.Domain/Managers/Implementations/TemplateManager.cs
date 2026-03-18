using System;
using CodeSectorCMS.Domain.Repositories;
using CodeSectorCMS.Domain.Managers.Interfaces;

namespace CodeSectorCMS.Domain.Managers.Implementations
{
    public class TemplateManager : ITemplateManager
    {
        private UnityOfWork unityOfWork;

        public TemplateManager(UnityOfWork unityOfWork)
        {
            this.unityOfWork = unityOfWork;
        }

        public IQueryable<Template> GetTemplateByID(int UserId, int TemplateID)
        {
       
            var template = unityOfWork.TemplateRepository.Get().Where(a => a.UserId == UserId).Where(a => a.TemplateID == TemplateID).AsQueryable();
            return template;

        }

      
        public List<Template> GetAllTemplate(int UserId)
        {
            var templates = unityOfWork.TemplateRepository.Get().Where(a => a.UserId == UserId).ToList();
            return templates;
          
        }

      
        public void CreateNewTemplate(Template template)
        {
            unityOfWork.TemplateRepository.Insert(template);
        
        }

       
       
        public void DeleteTemplateByID(int UserId, int TemplateID)
        {

            var templateToDelete = unityOfWork.TemplateRepository.Get().Where(c => c.UserId == UserId).Where(t => t.TemplateID == TemplateID).First();
            unityOfWork.TemplateRepository.Delete(templateToDelete.TemplateID);
        }

        public void UpdateTemplate(Template template)
        {
            unityOfWork.TemplateRepository.Update(template);
        }
        public void SaveChanges()
        {
            unityOfWork.Save();
        }

        public void Dispose()
        {
            unityOfWork.Dispose();
        }










    }
}
