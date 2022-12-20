using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Test_Task {
    /// <summary>
    /// Класс по работе с коллекцией тэгов
    /// </summary>
    internal class TagStorage {
        public TagItem Root = new TagItem(name:"Root", type:null);
        private const string xmlFilename = "tagTree.xml";

        /// <summary>
        /// Поиск по полному имени тэга
        /// </summary>
        /// <param name="tagPath">Полное имя тэга без учета Root тэга</param>
        /// <returns>Тэг класса TagItem</returns>
        public TagItem GetTag(string tagPath) {
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

        private void TagTreeFromXML(XElement parentElem, TagItem parentTag) {
            foreach (XElement childElem in parentElem.Elements("TagItem")) {
                Type valueType = Type.GetType(childElem.Attribute("Type").Value);
                TagItem childTag = new TagItem(name: childElem.Attribute("Name").Value, type: valueType,
                    value: Convert.ChangeType(childElem.Element("Value").Value, valueType), parentFullPath: parentTag.FullPath);
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

        private void TagTreeToXml(ref XElement parentElem, List<TagItem> tags) {
            if (tags.Count() > 0) {
                foreach (TagItem tag in tags) {
                    // Создание элемента для тега
                    XElement newTag = new XElement("TagItem",
                        new XAttribute("Name", tag.Name),
                        new XAttribute("Type", tag.ValueType),
                        new XElement("Value", tag.Value));

                    TagTreeToXml(ref newTag, tag.childNodes);

                    // Добавление элемента в дерево тэгов
                    parentElem.Add(newTag);
                }
            }
        }
    }
}
