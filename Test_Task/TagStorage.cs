using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Task {
    /// <summary>
    /// Класс по работе с коллекцией тэгов
    /// </summary>
    internal class TagStorage {
        public TagItem Root = new TagItem(name:"Root");
        private const string xmlPath = "tagTree.xml";

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

        }
    }
}
