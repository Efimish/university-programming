using BusinessLogic;
using Ninject;
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
        static void PrintSpec(int len, int maxLen)
        {
            string before = new string(' ', len);
            string after = new string(' ', maxLen - len);
            Console.BackgroundColor = ConsoleColor.Green;
            Console.Write(before);
            Console.BackgroundColor = ConsoleColor.Red;
            Console.Write(after);
            Console.ResetColor();
            Console.WriteLine();
        }
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
            IKernel ninjectKernel = new StandardKernel(new SimpleConfigModule());
            Logic logic = ninjectKernel.Get<Logic>();
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
                            bool deleted = logic.DeleteStudent(index);
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
                            List<(int, string, string, string)> students = logic.GetStudents();
                            if (students.Count() < 1)
                            {
                                Console.WriteLine("Здесь пусто :(");
                                break;
                            }
                            for(int i = 0; i < students.Count(); i++)
                            {
                                Console.WriteLine(students[i]);
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
                                int maxLen = gistogram.Values.Sum();
                                int maxStrLen = gistogram.Keys.Select(x => x.Length).Max();
                                foreach (var speciality in gistogram) {
                                    int strLen = speciality.Key.Length;
                                    Console.Write($"[{speciality.Key}]: " + new string(' ', maxStrLen - strLen));
                                    PrintSpec(speciality.Value, maxLen);
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
