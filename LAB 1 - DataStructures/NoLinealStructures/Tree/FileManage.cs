using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using LAB_1___DataStructures.NoLinealStructures.Tree;

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

        public BNode<T> CastNode(string path, int position, int grade)
        {
            int metadatelen = 118;
            int linelen = 90;
            BNode<T> node = new BNode<T>(grade);
            var buffer = new byte[linelen];
            using (var fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                fs.Seek((position - 1) * linelen + metadatelen, SeekOrigin.Begin);
                fs.Read(buffer, 0, linelen);
            }

            string node_text = Encoding.UTF8.GetString(buffer);

            int id = Convert.ToInt32(node_text.Substring(0, 20).Trim());
            int father = Convert.ToInt32(node_text.Substring(21, 25).Trim());
            string[] chlids = node_text.Substring(47, 25).Trim().Split(",");
            string[] values = node_text.Substring(73, 15).Trim().Split(",");


            return node;
        }
    }
}
