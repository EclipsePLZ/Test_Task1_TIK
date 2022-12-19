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
        public TagItem Root = new TagItem(name:"Root");
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
        public void LoadTagTreeFromXML() {

        }
        
        /// <summary>
        /// Выгрузка дерева тэгов в xml файл
        /// </summary>
        public void UploadTagTreeToXML() {
            XDocument xDoc = new XDocument();
            
            // Создаем корневой элемент
            XElement rootElem = new XElement("root");

            TagTreeToXml(ref rootElem, Root.childNodes);

            xDoc.Add(rootElem);
            xDoc.Save(xmlFilename);
        }

        private void TagTreeToXml(ref XElement parentElem, List<TagItem> tags) {
            foreach (TagItem tag in tags) {
                // Создание элемента для тега
                XElement newTag = new XElement("tag",
                    new XAttribute("name", tag.Name),
                    new XElement("value", tag.GetValue()));

                TagTreeToXml(ref newTag, tag.childNodes);

                // Добавление элемента в дерево тэгов
                parentElem.Add(newTag);
            }
        }
    }
}
