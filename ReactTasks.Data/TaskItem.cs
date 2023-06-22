using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactTasks.Data
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int? BeingDoneBy { get; set; }
        public bool Completed { get; set; }
        public string UserName { get; set; }

        public User User { get; set; }
    }

}
