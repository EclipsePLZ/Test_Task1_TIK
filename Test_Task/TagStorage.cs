using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Task {
    /// <summary>
    /// Класс TagStorage
    /// Позволяет работать с коллекцией тэгов
    /// </summary>
    internal class TagStorage {
        public TagItem Root = new TagItem("Root", "none");

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
    }
}
