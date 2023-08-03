using AppProjectDev.core.Models;
using Dapper;
using MySql.Data.MySqlClient;
using Npgsql;
using System;
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

        //Updates project description
        public void EditProject(ProjectModel project)
        {
            using (NpgsqlConnection conx = new NpgsqlConnection(SqlHelper.ConPostgreSQL))
            {
                conx.Open();
                conx.Execute("UPDATE project SET description = '" + project.Description + "' WHERE id = " + project.Id + ";");
            }
        }
        //Adds new project
        public void AddProject(ProjectModel project)
        {
            using (NpgsqlConnection conx = new NpgsqlConnection(SqlHelper.ConPostgreSQL))
            {
                conx.Open();

                conx.Execute("INSERT INTO project (name, description, is_complete) VALUES ('" + project.Name + "','" + project.Description + "', '0');");

            }
        }
        //Deletes projects
        public void RemoveProject(int id)
        {
            using (NpgsqlConnection conx = new NpgsqlConnection(SqlHelper.ConPostgreSQL))
            {
                conx.Open();
                conx.Execute("Delete from project_time where project_id like '" + id + "' ");
                conx.Execute("Delete from project where id like '" + id + "' ");
            }
        }
        //Retrieves uncompleted projects and descriptions
        public List<ProjectModel> GetProjects()
        {
            using (NpgsqlConnection conx = new NpgsqlConnection(SqlHelper.ConPostgreSQL))
            {
                conx.Open();
                List<ProjectModel> P = conx.Query<ProjectModel>("Select id, name, description, is_complete from project Where is_complete = '0'").AsList();
                foreach (ProjectModel p in P)
                {
                    try
                    {
                        int t = conx.QueryFirstOrDefault<int>("Select Sum(total_time) from project_time where project_id like '" + p.Id + "'");
                        p.Time = p.Time.Add(new TimeSpan(0, t, 0));
                    }
                    catch
                    {

                    }

                }
                return P;
            }
        }
        //Changes projects to complete
        public void Complete(int project)
        {
            using (NpgsqlConnection conx = new NpgsqlConnection(SqlHelper.ConPostgreSQL))
            {
                conx.Open();
                conx.Execute("Update project set is_complete = '1' where id like '" + project + "'");
            }
        }
        //Retreives users
        public List<UserModel> GetUsers()
        {
            using (NpgsqlConnection conx = new NpgsqlConnection(SqlHelper.ConPostgreSQL))
            {
                conx.Open();
                return conx.Query<UserModel>("Select id, name, username from users").AsList();

            }
        }
        //Starts a timer
        public void AddTime(int project, int user)
        {
            using (NpgsqlConnection conx = new NpgsqlConnection(SqlHelper.ConPostgreSQL))
            {
                conx.Open();
                conx.Execute("Insert into project_time (user_id, project_id, start_date, end_date, total_time) " +
                      "values(" + user + ",'" + project + "','" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "', '" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "' ,'0')");
            }
        }
        //Retrieves the total amount of time for any given project
        public List<TimeModel> GetTimeInProgress(int user)
        {
            using (NpgsqlConnection conx = new NpgsqlConnection(SqlHelper.ConPostgreSQL))
            {
                conx.Open();
                return conx.Query<TimeModel>("Select T.id, T.project_id, P.name, T.start_date, T.total_time from project_time T " +
                    "left outer join project P on T.project_id = P.id where user_id = " + user + " and total_time = 0").AsList();
            }
        }
        //Updates the end time for a specified project
        public void EndTime(TimeModel time)
        {
            using (NpgsqlConnection conx = new NpgsqlConnection(SqlHelper.ConPostgreSQL))
            {
                conx.Open();
                conx.Execute("Update project_time set end_date = '" + time.End_Date.ToString("yyyy-MM-dd hh:mm:ss") + "', total_time = '" + Math.Floor(time.Time.TotalMinutes) + "' where id = " + time.ID + "");
            }
        }
    }
}