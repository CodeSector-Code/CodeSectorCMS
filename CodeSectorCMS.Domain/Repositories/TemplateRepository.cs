using System;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace CodeSectorCMS.Domain.Repositories
{
    public class TemplateRepository : ITemplateRepository, IDisposable
    {
        private CmsContext context;

        public TemplateRepository(CmsContext context)
        {
            this.context = context;
        }

        public IEnumerable<Template> GetAllTemplates()
        {
            return context.Templates.ToList();
        }

        public Template GetTemplateByID(int TemplateID)
        {
            return context.Templates.Find(TemplateID);
        }

        public void InsertTemplate(Template template)
        {
            context.Templates.Add(template);
        }

        public void DeleteTemplate(int TemplateID)
        {
            Template templateToDelete = context.Templates.Find(TemplateID);
            context.Templates.Remove(templateToDelete);
        }

        public IEnumerable<Template> FindByName(string name)
        {
            return context.Templates.Where(t => t.Name.StartsWith(name)).ToList();
        }

        public IEnumerable<Template> FindBySubject(string subject)
        {
            return context.Templates.Where(t => t.Subject.StartsWith(subject)).ToList();
        }


        public void UpdateTemplate(Template template)
        {
            context.Entry(template).State = EntityState.Modified;
        }

        public void Save()
        {
            context.SaveChanges();
        }


         private bool disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


    }
}
