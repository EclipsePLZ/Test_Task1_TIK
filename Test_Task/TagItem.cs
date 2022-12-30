using System;
using System.Collections.Generic;

namespace Test_Task {
    /// <summary>
    /// Класс по работе с Тэгами
    /// </summary>
    internal class TagItem {
        /// <summary>
        /// Свойство Name определяет имя тэга
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Свойство Value определяет значения тэга
        /// </summary>
        public object Value { get; private set; }

        /// <summary>
        /// Свойство ValueType определяет тип значения тэга
        /// </summary>
        public Type ValueType { get; }

        /// <summary>
        /// Свойство Level определяет уровень вложенности тэга
        /// </summary>
        public int Level { get; }

        /// <summary>
        /// Свойство FullPath определяет полный путь к тэгу
        /// </summary>
        public string FullPath { get; private set; }

        /// <summary>
        /// Свойство childNodes определяет список дочерних тэгов
        /// </summary>
        public List<TagItem> childNodes { get; private set; }

        /// <summary>
        /// Конструктор Тэга
        /// </summary>
        /// <param name="name">Имя тэга</param>
        /// <param name="type">Тип тэга</param>
        /// <param name="value">Значение тэга</param>
        /// <param name="parentFullPath">Полный путь к родительскому тэгу</param>
        public TagItem(string name, Type type, object value = null, string parentFullPath = "") {
            if (name == "root") {
                FullPath = "root";
            }
            else {
                FullPath = parentFullPath + $".{name}";
            }
            Name = name;
            Value = value;
            ValueType = type;
            Level = parentFullPath.Split('.').Length;
            childNodes = new List<TagItem>();
        }

        /// <summary>
        /// Метод для записи значения тэга
        /// </summary>
        /// <param name="value">Новое значение тэга</param>
        public void SetValue(object value) {
            Value = value;
        }

        /// <summary>
        /// Метод для переименования тэга
        /// </summary>
        /// <param name="name">Новое имя тэга</param>
        public void SetName(string name) {
            FullPath = FullPath.Replace(Name, name);
            UpdateFullPathChild(Name + '.', name + '.');
            Name = name;
        }

        /// <summary>
        /// Обновление полного пути для всех дочерних тэгов
        /// </summary>
        /// <param name="oldParentName">Старое имя родительского тэга</param>
        /// <param name="newParentName">Новое имя родительского тэга</param>
        private void UpdateFullPathChild(string oldParentName, string newParentName) {
            FullPath = FullPath.Replace(oldParentName, newParentName);
            foreach (TagItem child in childNodes) {
                child.UpdateFullPathChild(oldParentName, newParentName);
            }
        }

        /// <summary>
        /// Добавление дочернего тэга
        /// </summary>
        /// <param name="childName">Имя дочернего тэга</param>
        /// <param name="valueType">Тип значения дочернего тэга</param>
        /// <param name="valueTag">Значение дочернего тэга</param>
        public void AddChildNode(string childName, Type valueType, object valueTag) {
            TagItem childNode = new TagItem(name:childName, type: valueType, value: valueTag, parentFullPath: FullPath);
            childNodes.Add(childNode);
        }

        /// <summary>
        /// Добавление дочернего тэга
        /// </summary>
        /// <param name="childNode">Дочерний тэг</param>
        public void AddChildNode(TagItem childNode) {
            childNodes.Add(childNode);
        }

        /// <summary>
        /// Удаление дочернего тэга
        /// </summary>
        /// <param name="childNode">Дочерний тэг</param>
        public void RemoveChildNode(TagItem childNode) {
            if (childNodes.Contains(childNode)) {
                childNodes.Remove(childNode);
            }
        }

        /// <summary>
        /// Доступ к дочернему тэгу по имени
        /// </summary>
        /// <param name="childName">Имя дочернего тэга</param>
        /// <returns></returns>
        public TagItem GetChildNode(string childName) {
            foreach (TagItem child in childNodes) {
                if (child.Name == childName) {
                    return child;
                }
            }
            return null;
        }
    }
}
