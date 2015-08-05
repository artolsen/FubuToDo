using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FubuTodoHelpers.Interfaces
{
    public interface IFubuTodoService
    {
        FubuTask LoadTask(int taskId);
        List<FubuTask> LoadTasks();
        void SaveTask(FubuTask task);
        void DeleteTask(int taskId);
    }
}
