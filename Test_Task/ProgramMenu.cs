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
        }

        /// <summary>
        /// Вывод меню программы
        /// </summary>
        private void ShowMenu() {
            Console.WriteLine("Введите номер действия:\n");
            Console.WriteLine("1. Загрузка дерева тэгов из XML файла.");
            Console.WriteLine("2. Выгрузка дерева тэгов в XML файл.");
            Console.WriteLine("3. Вывод списка тэгов.");
            Console.WriteLine("4. Удаление тэга.");
            Console.WriteLine("5. Добавление нового тэга.");
            Console.WriteLine("6. Переименование тэга.");
            Console.WriteLine("7. Вывести меню программы");
        }
    }
}
