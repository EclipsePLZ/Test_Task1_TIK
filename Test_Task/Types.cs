using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Task {
    internal class Types {
        private Dictionary<Type, string> typesString = new Dictionary<Type, string> {
            {typeof(int), "int" },
            {typeof(double), "double" },
            {typeof(bool), "bool" },
            {null, "none" }
        };

        private Dictionary<string, Type> stringTypes = new Dictionary<string, Type> {
            {"int", typeof(int)},
            {"double", typeof(double) },
            {"bool", typeof(bool) },
            {"none", null }
        };

        public Type GetType(string typeName) {
            return stringTypes[typeName];
        }

        public string TypeToString(Type type) {
            return typesString[type];
        }
    }
}
