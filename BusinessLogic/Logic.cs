using DataAccessLayer;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class Logic
    {
        private IRepository<Student> studentRepository;

        public Logic(IRepository<Student> repository) {
            studentRepository = repository;

            // энтити фрейм форк🍉
            // studentRepository = new EntityFrameworkRepository(new DataContext());

            //даппер. разкомментировать по необходимости🍌
            //studentRepository = new DapperRepository();
        }
        /// <summary>
        /// Добавить студента в список
        /// </summary>
        /// <param name="name">Имя студента</param>
        /// <param name="speciality">Специальность студента</param>
        /// <param name="group">Группа студента</param>
        public void AddStudent(string name, string speciality, string group) {
            Student student = new Student {
                Name = name,
                Speciality = speciality,
                Group = group
            };
            studentRepository.Create(student);
        }

        /// <summary>
        /// Удаляет студента из списка
        /// </summary>
        /// <param name="index">номер студента</param>
        /// <returns>true, если студент был удален, иначе - false</returns>
        public bool DeleteStudent(int index) {
            IEnumerable<Student> students = studentRepository.ReadAll();
            foreach (Student s in students) {
                if (s.Id == index) {
                    studentRepository.Delete(s);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Получить список студентов
        /// </summary>
        /// <returns>Список картежей по четыре элемента. (номер, имя, специальность, группа)</returns>
        public List<(int, string, string, string)> GetStudents()
        {
            IEnumerable<Student> students = studentRepository.ReadAll();
            return students.Select(x => (x.Id, x.Name, x.Speciality, x.Group)).ToList();
        }

        /// <summary>
        /// Получить статистику по студентам
        /// </summary>
        /// <returns>Словарь<специальность, кол-во студентов на ней></returns>
        public Dictionary<string, int> GetGistogram() {
            Dictionary<string, int> gistogram = new Dictionary<string, int>();
            IEnumerable<Student> students = studentRepository.ReadAll();
            foreach (Student student in students)
            {
                if (gistogram.ContainsKey(student.Speciality))
                {
                    gistogram[student.Speciality] += 1;
                }
                else
                {
                    gistogram[student.Speciality] = 1;
                }
            }
            return gistogram;
        }
    }
}
