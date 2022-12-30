using System;
using System.Collections.Generic;
using System.Linq;

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
            StartWork();
        }

        /// <summary>
        /// Запуск работы программы
        /// </summary>
        private void StartWork() {
            while (true) {
                ShowMenu();
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
                        StopWork();
                        break;
                    default:
                        Console.WriteLine("Недопустимое действие!");
                        break;
                }

                Console.WriteLine("\n");
            }
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
            Console.WriteLine("  7. Завершить программу.");
        }

        /// <summary>
        /// Загрузка дерева тэгов из xml-файла
        /// </summary>
        private void LoadTagsFromXML() {
            tagStorage = new TagStorage();
            if (tagStorage.LoadTagTreeFromXML()) {
                Console.WriteLine("Дерево тэгов успешно загружено");
            }
            else {
                Console.WriteLine("Не удалось загрузить дерево тэгов!");
            }
        }

        /// <summary>
        /// Выгрузка дерева тэгов в xml-файл
        /// </summary>
        private void UploadTagsToXML() {
            if (tagStorage.UploadTagTreeToXML()) {
                Console.WriteLine("Дерево тэгов успешно выгружено");
            }
            else {
                Console.WriteLine("Не удалось выгрузить дерево тэгов!");
            }
        }

        /// <summary>
        /// Вывод информации о тэгах в консоль
        /// </summary>
        /// <param name="tags">Список тэгов</param>
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

        /// <summary>
        /// Удаление тэга
        /// </summary>
        private void RemoveTag() {
            Console.WriteLine("Введите полное имя тэга:");
            string tagFullPath = Console.ReadLine();
            if (RemoveTagByPath(tagFullPath)) {
                Console.WriteLine("Тэг успешно удален");
            }
            else {
                Console.WriteLine("Не удалось найти тэг с таким именем");
            }
        }

        /// <summary>
        /// Удаление тэга по полному имени
        /// </summary>
        /// <param name="fullName">Полное имя тэга</param>
        /// <returns>Возвращает true если удаление выполнено успешно</returns>
        private bool RemoveTagByPath(string fullName) {
            // Получаем тэг для удаления
            TagItem tagToRemove = tagStorage.GetTag(fullName);
            if (tagToRemove == null) {
                return false;
            }

            // Получаем его родительский тэг
            TagItem parentTag = tagStorage.GetTag(fullName.Replace($".{tagToRemove.Name}", "")
                .Replace($"{tagToRemove.Name}", ""));

            parentTag.RemoveChildNode(tagToRemove);
            return true;
        }

        /// <summary>
        /// Добавление нового тэга
        /// </summary>
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
                // Получаем системный тип
                typeNewTag = GetType(typeTag);
            }
            catch {
                isSupportedType = false;
            }

            if (isSupportedType) {
                object value = null;
                bool isCorrectValue = true;
                if (typeNewTag != null) {
                    Console.WriteLine("Значение тэга (none - если не хранит значение):");
                    string valueInput = Console.ReadLine();
                    if (valueInput != "none") {
                        try {
                            value = Convert.ChangeType(valueInput, typeNewTag);
                        }
                        catch {
                            isCorrectValue = false;
                            Console.WriteLine("\nНекорретное значение тэга");
                        }
                    }
                }
                if (isCorrectValue) {
                    if (AddTag(parentFullPath, nameNewTag, typeNewTag, value)) {
                        Console.WriteLine("\nТэг успешно добавлен");
                    }
                    else {
                        Console.WriteLine("\nНе удалось добавить новый тэг (не существует родительский тэг)");
                    }
                }
            }
            else {
                Console.WriteLine("Вы ввели недопустимый тип тэга");
            }
        }

        /// <summary>
        /// Добавление нового тэга по полному имени родительского тэга
        /// </summary>
        /// <param name="parentFullPath">Полное имя родительского тэга</param>
        /// <param name="name">Имя нового тэга</param>
        /// <param name="type">Тип нового тэга</param>
        /// <param name="value">Значение нового тэга</param>
        /// <returns>Возвращает true если добавление выполнено успешно</returns>
        private bool AddTag(string parentFullPath, string name, Type type, object value) {
            // Получаем родительский тэг
            TagItem parentTag = tagStorage.GetTag(parentFullPath);
            if (parentTag == null) {
                return false;
            }
            parentTag.AddChildNode(name, type, value);
            return true;
        }

        /// <summary>
        /// Переименовать тэг
        /// </summary>
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

        /// <summary>
        /// Переименовать тэг по полному имени
        /// </summary>
        /// <param name="fullPath">Полное имя тэга</param>
        /// <param name="newName">Новое имя тэга</param>
        /// <returns></returns>
        private bool RenameTagByPath(string fullPath, string newName) {
            TagItem tag = tagStorage.GetTag(fullPath);
            if (tag == null) {
                return false;
            }
            tag.SetName(newName);
            return true;
        }

        /// <summary>
        /// Завершение работы программы
        /// </summary>
        private void StopWork() {
            System.Environment.Exit(1);
        }
    }
}
