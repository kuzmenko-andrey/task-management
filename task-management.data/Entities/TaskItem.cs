using System;
using System.Collections.Generic;
using System.Text;

namespace task_management.data.Entities
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string Assigne { get; set; }
    }
}
