using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using Model;

namespace DataAccessLayer
{
    /// <summary>
    /// CRUD using Dapper on Student object
    /// </summary>
    public class DapperRepository : IRepository<Student>
    {
        private string connectionString;
        public DapperRepository() {
            this.connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:" +
            "\\Users\\codyg\\source\\repos\\Лаба с Ефимом 3\\DataAccessLayer\\Database1.mdf;Integrated Security=True";
        }
        
        public void Create(Student obj)
        {
            string query = $"INSERT INTO Students (Name, Speciality, [Group]) " +
                $"VALUES (N'{obj.Name}', N'{obj.Speciality}', N'{obj.Group}')";
            Execute(query);
        }

        public void Delete(Student obj)
        {
            string query = $"DELETE FROM Students WHERE id = {obj.Id}";
            Execute(query);
        }

        public Student ReadById(int id)
        {
            //string query = $"SELECT * FROM student WHERE id = {id}";
            Student student;
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                student = db.Query<Student>($"SELECT * FROM Students WHERE Id = {id}").FirstOrDefault();
            }
            return student;
        }

        public IEnumerable<Student> ReadAll()
        {
            // query = $"SELECT * FROM student";
            List<Student> students;
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                students = db.Query<Student>("SELECT * FROM Students").ToList();
            }
            return students;
        }

        public void Update(Student obj)
        {
            string query = $"UPDATE Students" +
                $"SET Name = {obj.Name}, Speciality = {obj.Speciality}, Group = {obj.Group}" +
                $"WHERE id = {obj.Id}";
            Execute(query);
        }

        private void Execute(string query)
        {
            if (!string.IsNullOrEmpty(query))
            {
                using (IDbConnection db = new SqlConnection(connectionString))
                {
                    db.Execute(query);
                }
            }
        }
    }

}
