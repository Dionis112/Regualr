using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Regular.Console.Domain.Interfaces;
using System.Collections;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;

namespace Regular.Console.Handlers
{
    //to do Причесать код. Доработать универсальность.
    public class CsvSerializer<T> : ISerializer<T> where T : class
    {
        private char _separator = ';';

        /// <summary>
        /// Возвращает десериализованные объект
        /// </summary>
        /// <param name="path">Путь к файлу</param>
        /// <returns></returns>
        public T Deserialize(string path)
        {
            var lines = File.ReadAllLines(path, Encoding.GetEncoding("windows-1251"));
            var type = typeof(T);

            if (IsCollectionType(type))
            {
                Type[] arguments = GetGenericArguments(type);

                IList list = CreateList(arguments);

                var properties = arguments[0].GetProperties();

                foreach (var line in lines)
                {
                    var column = line.Split(';');

                    var item = Activator.CreateInstance(arguments[0]);

                    for (int i = 0; i < properties.Length; i++)
                    {
                        properties[i].SetValue(item, column[i]);
                    }

                    list.Add(item);
                }

                return (T)list;
            }

            return default(T);
        }

        private static Type[] GetGenericArguments(Type type)
        {
            var arg = type.GetGenericArguments();
            if (arg.Length == 0)
            {
                throw new Exception("Not Found Generic Arguments");
            }

            return arg;
        }

        private IList CreateList(Type[] arg)
        {
            return (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(arg[0]));
        }

        private bool IsEnumerableType(Type type)
        {
            return type.GetInterface(nameof(IEnumerable)) != null;
        }

        private bool IsCollectionType(Type type)
        {
            return type.GetInterface(nameof(ICollection)) != null;
        }

        public string Serialize(T item)
        {
            if (item is IEnumerable)
            {
                return this.Serialize((IEnumerable<T>)item);
            }

            var properties = typeof(T).GetProperties();

            GetHeader(properties);

            var values = properties.Select(s => s.GetValue(item)).ToArray();

            return string.Empty;
        }

        public string Serialize(IEnumerable<T> items)
        {
            StringBuilder rows = new StringBuilder();

            foreach (var item in items)
            {
                var row = this.Serialize(item);
                rows.Append(row);
            }

            return rows.ToString();
        }

        private string GetHeader(System.Reflection.PropertyInfo[] properties)
        {
            return string.Join(_separator, properties.Select(s => s.Name).ToArray());
        }


    }
}
