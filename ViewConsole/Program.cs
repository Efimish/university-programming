using BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ViewConsole
{
    internal class Program
    {
        static void PrintHelp() {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(
                "Введите команду:\n" +
                " - 1) Добавить нового студента\n" +
                " - 2) Удалить студента\n" +
                " - 3) Вывести всех студентов\n" +
                " - 4) Вывести гистограмму\n" +
                " - Escape) Выход"
            );
            Console.ResetColor();
        }
        static string ReadInput()
        {
            string input = Console.ReadLine();
            while (input.Length < 1)
            {
                Console.WriteLine("Строка не может быть пустой!");
                input = Console.ReadLine();
            }
            return input;
        }
        static void Main(string[] args)
        {
            Logic logic = new Logic();
            Console.OutputEncoding = Encoding.UTF8;
            while ( true )
            {
                PrintHelp();
                ConsoleKey key = Console.ReadKey().Key;
                Console.WriteLine();
                switch(key)
                {
                    case ConsoleKey.D1:
                        {
                            Console.WriteLine("Введите имя студента:");
                            string name = ReadInput();
                            Console.WriteLine("Введите специализацию студента:");
                            string speciality = ReadInput();
                            Console.WriteLine("Введите группу студента:");
                            string group = ReadInput();
                            logic.AddStudent( name, speciality, group );
                            Console.WriteLine("Студент успешно добавлен!");
                            break;
                        }
                    case ConsoleKey.D2:
                        {
                            if (logic.GetStudents().Count() < 1)
                            {
                                Console.WriteLine("Список студентов пуст");
                                break;
                            }
                            Console.WriteLine("Введите номер студента:");
                            string strIndex = ReadInput();
                            int index;
                            try
                            {
                                index = Convert.ToInt32(strIndex);
                            } catch (Exception _e)
                            {
                                Console.WriteLine("Введите число!");
                                break;
                            }
                            bool deleted = logic.DeleteStudent(index - 1);
                            if (deleted)
                            {
                                Console.WriteLine("Студент успешно удален!");
                            } else
                            {
                                Console.WriteLine("Такого студента нет!");
                            }
                            break;
                        }
                    case ConsoleKey.D3:
                        {
                            Console.WriteLine("Список студентов:");
                            List<(string, string, string)> students = logic.GetStudents();
                            if (students.Count() < 1)
                            {
                                Console.WriteLine("Здесь пусто :(");
                                break;
                            }
                            for(int i = 0; i < students.Count(); i++)
                            {
                                Console.WriteLine($"{i+1}) {students[i]}");
                            }
                            break;
                        }
                    case ConsoleKey.D4:
                        {
                            Console.WriteLine("Гистограмма студентов:");
                            Dictionary<string, int> gistogram = logic.GetGistogram();
                            if (gistogram.Count() < 1)
                            {
                                Console.WriteLine("Здесь пусто :(");
                            } else
                            {
                                foreach (var speciality in gistogram) {
                                    Console.WriteLine($"На направлении {speciality.Key} учится {speciality.Value} студентов;");
                                }
                            }
                            break;
                        }
                    case ConsoleKey.Escape:
                        {
                            Console.WriteLine("ППока !");
                            Environment.Exit(0);
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Нажмите 1-4 или Escape!!!");
                            break;
                        }
                }
            }
        }
    }
}
