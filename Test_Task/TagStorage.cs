using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Task {
    internal class TagStorage {
        public TagItem Root = new TagItem("Root", "none");

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
