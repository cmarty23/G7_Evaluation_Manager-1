using DBLayer;
using Evaluation_Manager.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evaluation_Manager.Repositories
{
    public class EvaluationRepository
    {
        public static Evaluation GetEvaluation(Student student, Activity activity)
        {
            Evaluation evaluation = null;
            string sql = $"SELECT * FROM Evaluations" +
                $" WHERE IdStudents = {student.Id} AND IdActivities = {activity.Id}";
            DB.OpenConnection();
            var reader = DB.GetDataReader(sql);
            if (reader.HasRows)
            {
                reader.Read();
                evaluation = CreateObject(reader);
                reader.Close();
            }
            DB.CloseConnection();
            return evaluation;
        }

        public static List<Evaluation> GetEvaluations(Student student)
        {
            List<Evaluation> evaluations = new List<Evaluation>();
            string sql = $"SELECT * FROM Evaluations WHERE IdStudents = {student.Id}";
            DB.OpenConnection();

            var reader = DB.GetDataReader(sql);
            while (reader.Read())
            {
                evaluations.Add(CreateObject(reader));
            }
            reader.Close();

            DB.CloseConnection();
            return evaluations;
        }

        private static Evaluation CreateObject(SqlDataReader dr)
        {
            return new Evaluation
            {
                Activity = ActivityRepository.GetActivity(
                    int.Parse(dr["IdActivities"].ToString())
                ),
                Student = StudentRepository.GetStudent(
                    int.Parse(dr["IdStudents"].ToString())
                ),
                Evaluator = TeacherRepository.GetTeacher(
                    int.Parse(dr["IdTeachers"].ToString())
                ),
                EvaluationDateTime = DateTime.Parse(dr["EvaluationDate"].ToString()),
                Points = int.Parse(dr["Points"].ToString())
            };
        }
    }
}
