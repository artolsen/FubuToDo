using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using StructureMap.Building;

namespace FubuTodo.EndPoints
{
    public class FubuTask
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string AssignedTo { get; set; }
        public DateTime? CompletedOn { get; set; }

        [JsonIgnore]
        public bool IsCompleted {
            get
            {
                return CompletedOn.HasValue;
            } 
        }

    }
}
