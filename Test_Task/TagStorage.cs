using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Test_Task {
    /// <summary>
    /// Класс по работе с коллекцией тэгов
    /// </summary>
    internal class TagStorage: Types {
        public TagItem Root = new TagItem(name:"root", type:null);
        private const string xmlFilename = "tagTree.xml";

        /// <summary>
        /// Поиск по полному имени тэга
        /// </summary>
        /// <param name="tagPath">Полное имя тэга без учета Root тэга</param>
        /// <returns>Тэг класса TagItem</returns>
        public TagItem GetTag(string tagPath) {
            if (tagPath == "") {
                return Root;
            }
            string[] tags = tagPath.Split('.');
            TagItem node = Root;

            foreach (string tag in tags) {
                node = node.GetChildNode(tag);
            }

            return node;
        }

        /// <summary>
        /// Загрузка дерева тэгов из xml файла
        /// </summary>
        public bool LoadTagTreeFromXML() {
            XDocument xDoc = new XDocument();
            XElement root;
            try {
                xDoc = XDocument.Load(xmlFilename);
                root = xDoc.Element("Root");
            }
            catch {
                return false;
            }

            TagTreeFromXML(root, Root);

            return true;
        }

        /// <summary>
        /// Формирование дерева тэгов из элементов xml-файла
        /// </summary>
        /// <param name="parentElem">Элемент xml-файла</param>
        /// <param name="parentTag">Тэг в дереве тэгов</param>
        private void TagTreeFromXML(XElement parentElem, TagItem parentTag) {
            foreach (XElement childElem in parentElem.Elements("TagItem")) {
                Type tagValueType;
                object tagValue;
                try {
                    tagValueType = GetType(childElem.Attribute("Type").Value);
                    if (childElem.Element("Value").Value == "") {
                        tagValue = null;
                    }
                    else {
                        tagValue = Convert.ChangeType(childElem.Element("Value").Value, tagValueType);
                    }
                }
                catch {
                    tagValueType = null;
                    tagValue = null;
                }
                
                TagItem childTag = new TagItem(name: childElem.Attribute("Name").Value, type: tagValueType,
                    value: tagValue, parentFullPath: parentTag.FullPath);
                parentTag.AddChildNode(childTag);

                TagTreeFromXML(childElem, childTag);
            }
        }
        
        /// <summary>
        /// Выгрузка дерева тэгов в xml файл
        /// </summary>
        public bool UploadTagTreeToXML() {
            XDocument xDoc = new XDocument();
            
            // Создаем корневой элемент
            XElement rootElem = new XElement("Root");

            TagTreeToXml(ref rootElem, Root.childNodes);

            xDoc.Add(rootElem);

            try {
                xDoc.Save(xmlFilename);
                return true;
            }
            catch {
                return false;
            }
        }

        /// <summary>
        /// Выгрузка списка тэгов в xml-файл
        /// </summary>
        /// <param name="parentElem">Родительский элемент xml-файла</param>
        /// <param name="tags">Список тэгов</param>
        private void TagTreeToXml(ref XElement parentElem, List<TagItem> tags) {
            if (tags.Count() > 0) {
                foreach (TagItem tag in tags) {
                    // Создание элемента для тега
                    XElement newTag = new XElement("TagItem",
                        new XAttribute("Name", tag.Name),
                        new XAttribute("Type", TypeToString(tag.ValueType)),
                        new XElement("Value", tag.Value));

                    TagTreeToXml(ref newTag, tag.childNodes);

                    // Добавление элемента в дерево тэгов
                    parentElem.Add(newTag);
                }
            }
        }
    }
}
