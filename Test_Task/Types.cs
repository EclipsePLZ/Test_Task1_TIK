using System;
using System.Collections.Generic;

namespace Test_Task {
    /// <summary>
    /// Класс по работе с типами тэгов
    /// </summary>
    internal class Types {
        private Dictionary<Type, string> typesString = new Dictionary<Type, string> {
            {typeof(int), "int" },
            {typeof(double), "double" },
            {typeof(bool), "bool" }
        };

        private Dictionary<string, Type> stringTypes = new Dictionary<string, Type> {
            {"int", typeof(int)},
            {"double", typeof(double) },
            {"bool", typeof(bool) },
            {"none", null }
        };

        /// <summary>
        /// Получить системный тип по строковому представлению
        /// </summary>
        /// <param name="typeName">Название типа</param>
        /// <returns>Возвращает тип в системном представлении</returns>
        public Type GetType(string typeName) {
            return stringTypes[typeName];
        }

        /// <summary>
        /// Получить строковое представление типа по системному представлению
        /// </summary>
        /// <param name="type">Системное представление типа</param>
        /// <returns>Возвращает строковое представление системного типа</returns>
        public string TypeToString(Type type) {
            if (type is null) {
                return "none";
            }
            return typesString[type];
        }
    }
}
