using AppProjectDev.core.Models;
using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppProjectDev.core.DataAccess
{
    public interface IDataRepository
    {
        public string GetTheme();
        public void AddProject(ProjectModel project);
        public void RemoveProject(int id);
        public List<ProjectModel> GetProjects();
        public List<UserModel> GetUsers();
        public List<TimeModel> GetTimeInProgress(int user);
        public void AddTime(int project, int user);
        public void EndTime(TimeModel time);
        public void Complete(int project);
        public void EditProject(ProjectModel project);
    }
    public class DataRepository : IDataRepository
    {
        //Selects dark theme on startup
        public string GetTheme()
        {
            return "FluentDark";

        }
        private static void Execute(string sql, object param = null)
        {
            using MySqlConnection conn = new(SqlHelper.ConMySQL);
            {
                conn.Execute(sql, param);
            }
        }
        private static IEnumerable<T> Query<T>(string sql, object param = null)
        {
            using MySqlConnection conn = new(SqlHelper.ConMySQL);
            {
                return conn.Query<T>(sql, param);
            }
        }
        private static T QueryFirst<T>(string sql, object param = null)
        {
            using MySqlConnection conn = new(SqlHelper.ConMySQL);
            {
                return conn.QueryFirstOrDefault<T>(sql, param);
            }
        }
        //Updates project description
        public void EditProject(ProjectModel project)
        { 
                Execute("UPDATE Project SET description = '" + project.Description + "' WHERE id = " + project.Id + ";");
        }
        //Adds new project
        public void AddProject(ProjectModel project)
        {
            Execute("INSERT INTO Project (name, description, is_complete) VALUES ('" + project.Name + "','" + project.Description + "', '0');");
        }
        //Deletes projects
        public void RemoveProject(int id)
        {
            Execute("Delete from Project_Time where project_id = " + id + "");
            Execute("Delete from Project where id = " + id + "");
        }
        //Retrieves uncompleted projects and descriptions
        public List<ProjectModel> GetProjects()
        {
            List<ProjectModel> P = Query<ProjectModel>("Select id, name, description, is_complete from Project Where is_complete = '0'").AsList();
            foreach (ProjectModel p in P)
            {
                try
                {
                    int t = QueryFirst<int>("Select Sum(total_time) from Project_Time where project_id like '" + p.Id + "'");
                    p.Time = p.Time.Add(new TimeSpan(0, t, 0));
                }
                catch
                {

                }
            }
            return P;
        }
        //Changes projects to complete
        public void Complete(int project)
        {
            Execute("Update Project set is_complete = 1 where id = " + project + "");
        }
        //Retreives users
        public List<UserModel> GetUsers()
        {
                return Query<UserModel>("Select * from Project_User").AsList();
        }
        //Starts a timer
        public void AddTime(int project, int user)
        {
            Execute("Insert into Project_Time (user_id, project_id, start_date, end_date, total_time) " +
                      "values(" + user + ",'" + project + "','" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "', '" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "' ,'0')");
        }
        //Retrieves the total amount of time for any given project
        public List<TimeModel> GetTimeInProgress(int user)
        {
            return Query<TimeModel>("Select T.id, T.project_id, P.name, T.start_date, T.total_time from Project_Time T " +
                    "left outer join Project P on T.project_id = P.id where user_id = " + user + " and total_time = 0").AsList();
        }
        //Updates the end time for a specified project
        public void EndTime(TimeModel time)
        {
            Execute("Update Project_Time set end_date = '" + time.End_Date.ToString("yyyy-MM-dd hh:mm:ss") + "', total_time = '" + Math.Floor(time.Time.TotalMinutes) + "' where id = " + time.ID + "");
        }
    }
}