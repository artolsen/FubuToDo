using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FubuTodoHelpers;

namespace FubuTodo.EndPoints
{
    public class IndexViewModel
    {
        private List<FubuTask> _tasks = new List<FubuTask>();

        public List<FubuTask> Tasks { get; set; }
    }

   
}
