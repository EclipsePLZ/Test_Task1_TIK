using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Task {
    /// <summary>
    /// Класс по работе в консольном приложении
    /// </summary>
    internal class ProgramMenu: Types {
        private TagStorage tagStorage = new TagStorage();

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
                        LoadTagsFromXML();
                        break;
                    case "2":
                        UploadTagsToXML();
                        break;
                    case "3":
                        PrintTagTree(tagStorage.Root.childNodes);
                        break;
                    case "4":
                        RemoveTag();
                        break;
                    case "5":
                        AddNewTag();
                        break;
                    case "6":
                        RenameTag();
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

        private void LoadTagsFromXML() {
            if (tagStorage.LoadTagTreeFromXML()) {
                Console.WriteLine("Дерево тэгов успешно загружено");
            }
            else {
                Console.WriteLine("Не удалось загрузить дерево тэгов!");
            }
        }

        private void UploadTagsToXML() {
            if (tagStorage.UploadTagTreeToXML()) {
                Console.WriteLine("Дерево тэгов успешно выгружено");
            }
            else {
                Console.WriteLine("Не удалось выгрузить дерево тэгов!");
            }
        }

        private void PrintTagTree(List<TagItem> tags) {
            if (tags.Count() > 0) {
                foreach (TagItem tag in tags) {
                    Console.WriteLine($"Полный путь: {tag.FullPath.Replace("root.", "")}");
                    Console.WriteLine($"Уровнь вложенности: {tag.Level}");
                    Console.WriteLine($"Тип значения: {TypeToString(tag.ValueType)}");
                    Console.WriteLine($"Значение: {tag.Value}\n");

                    PrintTagTree(tag.childNodes);
                }
            }
        }

        private void RemoveTag() {
            Console.WriteLine("Введите полное имя тэга:");
            string tagFullPath = Console.ReadLine();
            if (RemoveTagByPath(tagFullPath)) {
                Console.WriteLine("Тэг успешно удален");
            }
            else {
                Console.WriteLine("Не удалось удалить данный тэг");
            }
        }

        private bool RemoveTagByPath(string fullName) {
            TagItem tagToRemove;
            try {
                tagToRemove = tagStorage.GetTag(fullName);
            }
            catch {
                return false;
            }
            TagItem parentTag = tagStorage.GetTag(fullName.Replace($".{tagToRemove.Name}", ""));
            parentTag.RemoveChildNode(tagToRemove);
            return true;
        }

        private void AddNewTag() {
            Console.WriteLine("Полное имя родительского тэга:");
            string parentFullPath = Console.ReadLine();
            Console.WriteLine("Имя нового тэга:");
            string nameNewTag = Console.ReadLine();
            Console.WriteLine("Тип нового тэга (double, int, bool, none):");
            string typeTag = Console.ReadLine().ToLower();
            Type typeNewTag = null;
            bool isSupportedType = true;

            try {
                typeNewTag = GetType(typeTag);
            }
            catch {
                isSupportedType = false;
            }

            if (isSupportedType) {
                object value = null;
                bool isCorrectValue = true;
                if (typeNewTag != null) {
                    Console.WriteLine("\nЗначение тэга:");
                    try {
                        value = Convert.ChangeType(Console.ReadLine(), typeNewTag);
                    }
                    catch {
                        isCorrectValue = false;
                        Console.WriteLine("\nНекорретное значение тэга");
                    }
                }
                if (isCorrectValue && AddTag(parentFullPath, nameNewTag, typeNewTag, value)) {
                    Console.WriteLine("\nТэг успешно добавлен");
                }
                else {
                    Console.WriteLine("\nНе удалось добавить новый тэг");
                }
            }
            else {
                Console.WriteLine("Вы ввели недопустимый тип тэга");
            }
        }

        private bool AddTag(string parentFullPath, string name, Type type, object value) {
            try {
                TagItem parentTag = tagStorage.GetTag(parentFullPath);
                parentTag.AddChildNode(name, type, value);
                return true;
            }
            catch {
                return false;
            }
        }

        private void RenameTag() {
            Console.WriteLine("Полный путь:");
            string fullPath = Console.ReadLine();
            Console.WriteLine("Новое имя тэга:");
            string newName = Console.ReadLine();

            if (RenameTagByPath(fullPath, newName)) {
                Console.WriteLine("\nТэг успешно переименован");
            }
            else {
                Console.WriteLine("\nНеудалось переименовать тэг");
            }
        }

        private bool RenameTagByPath(string fullPath, string newName) {
            try {
                TagItem tag = tagStorage.GetTag(fullPath);
                tag.SetName(newName);
                return true;
            }
            catch {
                return false;
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
