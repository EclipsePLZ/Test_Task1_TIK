using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Task {
    /// <summary>
    /// Класс по работе в консольном приложении
    /// </summary>
    internal class ProgramMenu {
        /// <summary>
        /// Запуск консольного меню
        /// </summary>
        public void StartMenu() {
            ShowMenu();
            StartWork();
        }

        /// <summary>
        /// Вывод меню программы
        /// </summary>
        private void ShowMenu() {
            Console.WriteLine("Доступные действия:");
            Console.WriteLine("  1. Загрузка дерева тэгов из XML файла.");
            Console.WriteLine("  2. Выгрузка дерева тэгов в XML файл.");
            Console.WriteLine("  3. Вывод списка тэгов.");
            Console.WriteLine("  4. Удаление тэга.");
            Console.WriteLine("  5. Добавление нового тэга.");
            Console.WriteLine("  6. Переименование тэга.");
            Console.WriteLine("  7. Вывести меню программы.");
            Console.WriteLine("  8. Завершить программу.");
        }

        /// <summary>
        /// Запуск работы программы
        /// </summary>
        private void StartWork() {
            while (true) {
                Console.WriteLine("\n\nВведите номер действия:");
                string action = Console.ReadLine();
                Console.WriteLine();

                switch (action) {
                    case "1":
                        break;
                    case "2":
                        break;
                    case "3":
                        break;
                    case "4":
                        break;
                    case "5":
                        break;
                    case "6":
                        break;
                    case "7":
                        ShowMenu();
                        break;
                    case "8":
                        StopWork();
                        break;
                    default:
                        Console.WriteLine("Недопустимое действие!");
                        break;
                }
            }
        }

        /// <summary>
        /// Завершение работы программы
        /// </summary>
        private void StopWork() {
            System.Environment.Exit(1);
        }
    }
}
