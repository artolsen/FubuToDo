using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raven.Imports.Newtonsoft.Json;

namespace FubuTodoHelpers
{
    public class FubuTask
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string AssignedTo { get; set; }
        public DateTime? CompletedOn { get; set; }

        [JsonIgnore]
        public bool IsCompleted
        {
            get
            {
                return CompletedOn.HasValue;
            }
        }
    }
}
