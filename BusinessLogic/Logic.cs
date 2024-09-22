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
        private List<Student> students;
        public Logic() {
            this.students = new List<Student>();
            students.Add(new Student {
                Name = "Student 1",
                Speciality = "ПИ",
                Group = "КИ23-20"
            });
            students.Add(new Student {
                Name = "Student 2",
                Speciality = "ПИ",
                Group = "КИ23-20"
            });
            students.Add(new Student {
                Name = "Student 3",
                Speciality = "ПИ",
                Group = "КИ23-21"
            });
            students.Add(new Student {
                Name = "Student 4",
                Speciality = "ИБ",
                Group = "КИ23-10"
            });
        }
        /// <summary>
        /// Добавить студента в список
        /// </summary>
        /// <param name="name">Имя студента</param>
        /// <param name="speciality">Специальность студента</param>
        /// <param name="group">Группа студента</param>
        public void AddStudent(string name, string speciality, string group) {
            this.students.Add(new Student {
                Name = name,
                Speciality = speciality,
                Group = group
            });
        }
        /// <summary>
        /// Удаляет студента из списка
        /// </summary>
        /// <param name="index">номер студента</param>
        /// <returns>true, если студент был удален, иначе - false</returns>
        public bool DeleteStudent(int index) {
            int length = this.students.Count();
            if (index < 0 || index >= length) {
                return false;
            }
            this.students.RemoveAt(index);
            return true;
        }
        /// <summary>
        /// Получить список студентов
        /// </summary>
        /// <returns>Список картежей по три элемента. (имя, специальность, группа)</returns>
        public List<(string, string, string)> GetStudents()
        {
            return this.students.Select(x => (x.Name, x.Speciality, x.Group)).ToList();
        }
        /// <summary>
        /// Получить статистику по студентам
        /// </summary>
        /// <returns>Словарь<специальность, кол-во студентов на ней></returns>
        public Dictionary<string, int> GetGistogram() {
            Dictionary<string, int> gistogram = new Dictionary<string, int>();
            foreach (Student student in this.students)
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
