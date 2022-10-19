using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Task {
    /// <summary>
    /// Класс TagItem
    /// Позволяет работать с Тэгами
    /// </summary>
    internal class TagItem {
        /// <summary>
        /// Свойство Name определяет имя тэга
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Свойство TypeValue определяет тип хранимого значения
        /// </summary>
        private string TypeValue { get; set; }

        /// <summary>
        /// Свойство Value определяет значения тэга
        /// </summary>
        private object Value { get; set; }

        /// <summary>
        /// Свойство Level определяет уровень вложенности тэга
        /// </summary>
        private int Level { get; }

        /// <summary>
        /// Свойство FullPath определяет полный путь к тэгу
        /// </summary>
        private string FullPath { get; }

        /// <summary>
        /// Поле childNodes содержит список дочерних тэгов
        /// </summary>
        private List<TagItem> childNodes;

        public TagItem(string name, string typeValue, object value = null, int level = 0, string fullPath = "") {
            Name = name;
            TypeValue = typeValue;
            Value = null;
            Level = level;
            FullPath = fullPath;
            childNodes = new List<TagItem>();
        }

        public string GetValueType() {
            return TypeValue;
        }

        public object GetValue() {
            return Value;
        }

        public void SetValue(object value) {
            Value = value;
        }

        public void SetName(string name) {
            Name = name;
        }

        public void AddChildNode(TagItem childNode) {
            childNodes.Add(childNode);
        }

        public void RemoveChildNode(TagItem childNode) {
            if (childNodes.Contains(childNode)) {

                // Cascading removal of child nodes
                foreach (TagItem child in childNode.childNodes) {
                    childNode.RemoveChildNode(child);
                }

                childNodes.Remove(childNode);
            }
        }

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
