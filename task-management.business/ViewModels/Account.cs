using System;
using System.Collections.Generic;
using System.Text;

namespace task_management.business.ViewModels
{
    public class Account
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public string Password { get; set; }
        //photo
        public string Username { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public string Email { get; set; }
        public string Position { get; set; }
    }
}
