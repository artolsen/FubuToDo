using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FubuTodo.EndPoints
{
    public class AddEditInputViewModel
    {
        public int Id { get; set; }
        public string ErrorMessage { get; set; }
        public string Description { get; set; }
        public string AssignedTo { get; set; }
        public DateTime? CompletedDate { get; set; }
        public bool IsCompleted { get; set; }
    }
}
