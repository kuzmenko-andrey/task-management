using System;
using System.Collections.Generic;
using System.Text;

namespace task_management.business.ViewModels
{
    public class TaskItem
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string Assignee { get; set; }
    }
}
