using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using LAB_1___DataStructures.NoLinealStructures.Tree;
using System.Data.SqlTypes;
using System.Text.Json.Serialization;
using System.Runtime.InteropServices;
using System.Linq;
using System.Xml.Serialization;
using System.Text.Encodings.Web;

namespace LAB_1___DataStructures
{
    public class FileManage<T>
    {
        public int LineLength { get; set; }
        public string Path { get; set; }
        public int MetadataLength { get; set; }
        public int Grade { get; set; }
        public int FieldLength { get; set; }
        public Delegate ConvertValues;
        public Delegate GetValues;


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
        public void WriteNode(BNode<T> node)
        {
            string test = node.ToString();
            test += (string)GetValues.DynamicInvoke(node.Values, FieldLength);
            byte[] bytes = Encoding.ASCII.GetBytes(test);
            int position = node.Id;
            using (var fs = new FileStream(Path, FileMode.OpenOrCreate))
            {
                fs.Seek((position - 1) * LineLength + MetadataLength, SeekOrigin.Begin);
                fs.Write(bytes, 0, bytes.Length);
            }
        }
        public BNode<T> CastNode(int position)
        {
            try
            {
                var buffer = new byte[LineLength];
                using (var fs = new FileStream(Path, FileMode.OpenOrCreate))
                {
                    fs.Seek((position - 1) * LineLength + MetadataLength, SeekOrigin.Begin);
                    fs.Read(buffer, 0, LineLength);
                }

                string node_text = Encoding.UTF8.GetString(buffer);

                int id = int.Parse(node_text.Substring(0, 10).Trim());
                string test = node_text.Substring(12, 10).Trim();
                int father = int.Parse(node_text.Substring(12, 9).Trim());
                var childsString = node_text.Substring(22, 30).Trim().Split(',').ToList<string>();

                List<int> childs = new List<int>();
                foreach (var item in childsString)
                {
                    childs.Add(int.Parse(item));
                }
                
                //List<string> valuesString = new List<string>(Split(tempVal, FieldLength));
                //List<string> valuesString = new List<string>();
                //var valsTem = node_text.Substring(53).Replace('\0', ' ').TrimEnd();
                //for (int i = 53; i < node_text.Length; i += FieldLength)
                //{
                //    valuesString.Add(valsTem.Substring(i, FieldLength).Trim());
                //}

                // tempVal = node_text.Substring(53).Replace("\n", " ").Replace("\0", " ").Trim();
                List<T> valuesT = (List<T>)ConvertValues.DynamicInvoke(valuesString);

                BNode <T> newNode = new BNode<T>(Grade);
                newNode.Id = id;
                newNode.Father = father;
                newNode.Childs = childs;
                newNode.Values = valuesT;

                return newNode;
            }
            catch (Exception)
            {

                return null;
            }
        }
        static IEnumerable<string> Split(string str, int fieldlegth)
        {

            return Enumerable.Range(0, str.Length / fieldlegth).Select(i => str.Substring(i * fieldlegth, fieldlegth));
        }
    }
}
