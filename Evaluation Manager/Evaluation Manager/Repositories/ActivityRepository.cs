using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBLayer;
using Evaluation_Manager.Models;

namespace Evaluation_Manager.Repositories
{
    public static class ActivityRepository
    {
        public static Activity GetActivity(int id)
        {
            Activity activity = null;
            string sql = $"SELECT * FROM Activities WHERE Id = {id}";
            DB.OpenConnection();
            var reader = DB.GetDataReader(sql);
            if (reader.HasRows)
            {
                reader.Read();
                activity = CreateObject(reader);
                reader.Close();
            }
            DB.CloseConnection();
            return activity;
        }

        public static List<Activity> GetActivities()
        {
            List<Activity> activities = new List<Activity>();
            string sql = "SELECT * FROM Activities";
            DB.OpenConnection();
            var reader = DB.GetDataReader(sql);
            while (reader.Read())
            {
                Activity activity = CreateObject(reader);
                activities.Add(activity);
            }
            reader.Close();
            DB.CloseConnection();
            return activities;
        }

        private static Activity CreateObject(SqlDataReader reader)
        {



            int id = int.Parse(reader["Id"].ToString());
            string name = reader["Name"].ToString();
            string description = reader["Description"].ToString();
            int.TryParse(reader["MaxPoints"].ToString(), out int MaxPoints);
            int.TryParse(reader["MinPointsForGrade"].ToString(), out int MinPointsForGrade);
            int.TryParse(reader["MinPointsForSignature"].ToString(), out int MinPointsForSignature);

            var activity = new Activity
            {
                Id = id,
                Name = name,
                Description = description,
                MaxPoints = MaxPoints,
                MinPointsForGrade = MinPointsForGrade,
                MinPointsForSignature = MinPointsForSignature
            };
            return activity;

        }
    }
}
