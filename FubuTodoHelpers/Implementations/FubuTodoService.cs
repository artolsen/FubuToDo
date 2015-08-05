using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FubuTodoHelpers.Interfaces;
using Raven.Client;
using Raven.Client.Document;

namespace FubuTodoHelpers.Implementations
{
    public class FubuTodoService: IFubuTodoService
    {
        private IDocumentSession _documentSession;
        public FubuTodoService(IDocumentSession documentSession)
        {
            _documentSession = documentSession;
        }

        public FubuTask LoadTask(int taskId)
        {
            return _documentSession.Load<FubuTask>(taskId);
        }

        public List<FubuTask> LoadTasks()
        {
            return _documentSession.Query<FubuTask>().Customize(x => x.WaitForNonStaleResults()).ToList();
        }

        public void SaveTask(FubuTask task)
        {
            FubuTask existing = new FubuTask();
            if (task.Id > 0)
            {
                existing = _documentSession.Load<FubuTask>(task.Id);
                existing.Description = task.Description;
                existing.AssignedTo = task.AssignedTo;
                existing.CompletedOn = task.CompletedOn;
            }
            else
            {
                existing = task;
            }

            _documentSession.Store(existing);
            _documentSession.SaveChanges();
        }

        public void DeleteTask(int taskId)
        {
            _documentSession.Delete<FubuTask>(taskId);
            _documentSession.SaveChanges();
        }
    }
}
