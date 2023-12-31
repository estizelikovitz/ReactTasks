﻿using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using ReactTasks.Web.Models;
using Microsoft.AspNetCore.Http;

namespace ReactTasks.Data
{
    public class TasksHub : Hub
    {


        private string _connectionString;

        public TasksHub(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }



        public void NewTask(string taskTitle)
        {
            var repo = new Repository(_connectionString);
            TaskItem t = new();
            t.Title = taskTitle;
            t.Completed = false;
            repo.AddTask(t);
            List<TaskItem> tasks = repo.GetTasksNotDone();
            Clients.All.SendAsync("refreshTasks", tasks);
        }

        public void Completed(TaskItem task)
        {
            var repo = new Repository(_connectionString);
            repo.Completed(task);
            List<TaskItem> tasks = repo.GetTasksNotDone();
            Clients.All.SendAsync("refreshTasks", tasks);
        }

        public void Choose(TaskItem task)
        {
            var repo = new Repository(_connectionString);
            var user = repo.GetByEmail(Context.User.Identity.Name);
            repo.Choose(task, user);
            List<TaskItem> tasks = repo.GetTasksNotDone();
            Clients.All.SendAsync("refreshTasks", tasks);
        }
        public void GetAll()
        {
            var repo = new Repository(_connectionString);
            List<TaskItem> tasks = repo.GetTasksNotDone();
            Clients.All.SendAsync("refreshTasks", tasks);
        }


    }
}
