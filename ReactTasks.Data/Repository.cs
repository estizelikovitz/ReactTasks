using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactTasks.Data
{
    public class Repository
    {
        private string _connectionString;
        public Repository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void AddUser(User user)
        {
            var hash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
            user.PasswordHash = hash;
            using var context = new TasksDataContext(_connectionString);
            context.Users.Add(user);
            context.SaveChanges();
        }

        public void AddTask(TaskItem task)
        {
            using var ctx = new TasksDataContext(_connectionString);
            ctx.Tasks.Add(task);
            ctx.SaveChanges();
        }
        public User Login(string email, string password)
        {
            using var context = new TasksDataContext(_connectionString);
            var user = context.Users.FirstOrDefault(u => u.Email == email);
            if (user == null)
            {
                return null;
            }

            bool isValid = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
            return isValid ? user : null;
        }
        public User GetByEmail(string email)
        {
            using var ctx = new TasksDataContext(_connectionString);
            return ctx.Users.FirstOrDefault(u => u.Email == email);
        }
        public List<TaskItem> GetTasksNotDone()
        {
            using var context = new TasksDataContext(_connectionString);
            return context.Tasks.Where(t => t.Completed == false).ToList();
        }
        public void Completed(TaskItem task)
        {
            using var context = new TasksDataContext(_connectionString);
            context.Tasks.FirstOrDefault(t => t == task).Completed = true;
            context.SaveChanges();

        }

        public void Choose(TaskItem task, User user)
        {
            using var context = new TasksDataContext(_connectionString);
            context.Tasks.FirstOrDefault(t => t == task).User = user;
            context.Tasks.FirstOrDefault(t => t == task).UserName = user.Name;
            context.Tasks.FirstOrDefault(t => t == task).BeingDoneBy = user.Id;
            context.SaveChanges();

        }
    }
}
