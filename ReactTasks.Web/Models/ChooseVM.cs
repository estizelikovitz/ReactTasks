using ReactTasks.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactTasks.Web.Models
{
    public class ChooseVM
    {
        public TaskItem Task { get; set; }
        public User User { get; set; }
    }
}
