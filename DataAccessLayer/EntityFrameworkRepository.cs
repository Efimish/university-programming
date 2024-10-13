using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Data.Entity;

namespace DataAccessLayer
{
    public class DataContext: DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DataContext() : base("Data Source=(LocalDB)\\" +
            "MSSQLLocalDB;AttachDbFilename=C:\\Users\\codyg\\source\\repos\\Лаба_С_Ефимом_2\\" +
            "DataAccessLayer\\Database1.mdf;Integrated Security=True")
        { }
    }

    /// <summary>
    /// CRUD using Entity on Student object
    /// </summary>
    public class EntityFrameworkRepository : IRepository<Student>
    {
        //string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\codyg\\source\\repos\\Лаба_С_Ефимом_2\\DataAccessLayer\\Database1.mdf;Integrated Security=True";
        DataContext _context;
        public EntityFrameworkRepository(DataContext context)
        {
            _context = context;
        }

        public void Create(Student obj)
        {
            _context.Set<Student>().Add(obj);
            _context.SaveChanges();
        }

        public void Delete(Student obj)
        {
            _context.Set<Student>().Remove(obj);
            _context.SaveChanges();
        }

        public IEnumerable<Student> ReadAll()
        {
            return new List<Student>(_context.Set<Student>());
        }

        public Student ReadById(int id)
        {
            return _context.Students.Where(x => x.Id == id).FirstOrDefault();
        }

        public void Update(Student obj)
        {
            Student orig = _context.Students.Where(x => x.Id == obj.Id).FirstOrDefault();
            orig.Name = obj.Name;
            orig.Speciality = obj.Speciality;
            orig.Group = obj.Group;
            _context.SaveChanges();
        }
    }
}
