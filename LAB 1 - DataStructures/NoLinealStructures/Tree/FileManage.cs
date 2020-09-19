using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using LAB_1___DataStructures.NoLinealStructures.Tree;
using System.Data.SqlTypes;
using System.Text.Json.Serialization;
using System.Runtime.InteropServices;
using System.Linq;

namespace LAB_1___DataStructures
{
    class FileManage<T>
    {
        public int LineLength { get; set; }

        public int[] ReadProperties(string path)
        {
            
            int[] properties = new int[3];
            using (StreamReader fs = new StreamReader(path))
            {
                for (int i = 0; i < 3; i++)
                {
                    string[] value = fs.ReadLine().Split(":");
                    properties[i] = Convert.ToInt32(value[1]);
                }
            }
            return properties;
        }

        public BNode<T> CastNode(string path, int position, int grade, Delegate ConvertValues)
        {
            int metadatelen = 118;
            int linelen = 90;
            var buffer = new byte[linelen];
            using (var fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                fs.Seek((position - 1) * linelen + metadatelen, SeekOrigin.Begin);
                fs.Read(buffer, 0, linelen);
            }

            string node_text = Encoding.UTF8.GetString(buffer);

            int id = int.Parse(node_text.Substring(0, 20).Trim());
            int father = int.Parse(node_text.Substring(21, 25).Trim());
            var childsString = node_text.Substring(47, 25).Trim().Split(',').ToList<string>();

            List<int> childs = new List<int>();
            foreach (var item in childsString)
            {
                childs.Add(int.Parse(item));
            }
            var valuesString = node_text.Substring(73, 15).Trim().Split(',').ToList<string>();
            List<T> valuesT = (List<T>)ConvertValues.DynamicInvoke(valuesString);

            BNode <T> newNode = new BNode<T>(id, father, childs, valuesT);

            return newNode;
        }
    }
}
