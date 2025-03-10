﻿using System;
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
        public int MetaDataLength => 125;
        public int FieldLength { get; set; }
        public int LineLength { get; set; }
        public int Grade { get; set; }
        public string Path { get; set; }
        public Delegate ValueConverter { get; set; }
        public Delegate ValueDeconverter { get; set; }

        public int[] ReadProperties()
        {
            int[] properties = new int[3];
            using (StreamReader fs = new StreamReader(Path))
            {
                for (int i = 0; i < 3; i++)
                {
                    string[] value = fs.ReadLine().Split(":");
                    properties[i] = Convert.ToInt32(value[1]);
                }
            }
            return properties;
        }
        public void UpdateProperties(int root, int next_id)
        {
            string w_root = $"root:    {string.Format("{0, -3}", root)}";
            string w_id =   $"next_id:{string.Format("{0, -3}", next_id)}";
            byte[] line_one = Encoding.UTF8.GetBytes(w_root);
            using (var fs = new FileStream(Path, FileMode.OpenOrCreate))
            {
                fs.Seek(0, SeekOrigin.Begin);
                fs.Write(line_one, 0, w_root.Length);
            }
            byte[] line_two = Encoding.UTF8.GetBytes(w_id);
            using (var fs = new FileStream(Path, FileMode.OpenOrCreate))
            {
                fs.Seek(14, SeekOrigin.Begin);
                fs.Write(line_two, 0, w_id.Length);
            }
        }

        public void UpdateGrade(int grade)
        {
            string w_grade = $"grade:  {string.Format("{0,-5}", grade)}";
            byte[] line_three = Encoding.UTF8.GetBytes(w_grade);
            using (var fs = new FileStream(Path, FileMode.OpenOrCreate))
            {
                fs.Seek(28, SeekOrigin.Begin);
                fs.Write(line_three, 0, w_grade.Length);
            }
        }

        public BNode<T> CastNode(int position)
        {
            var buffer = new byte[LineLength];
            using (var fs = new FileStream(Path, FileMode.OpenOrCreate))
            {
                fs.Seek((position - 1) * LineLength + MetaDataLength, SeekOrigin.Begin);
                fs.Read(buffer, 0, LineLength);
            }

            string node_text = Encoding.UTF8.GetString(buffer);

            //REMIENDO
            node_text = node_text.Substring(1, node_text.Length-1);
            node_text = node_text.Replace("\0", " ");
            node_text = node_text.Substring(0, (LineLength-2));

            if (node_text.Substring(0, 20).Trim() == "") return null;

            int id = Convert.ToInt32(node_text.Substring(0, 20).Trim());
            int father = Convert.ToInt32(node_text.Substring(21, 25).Trim());
            string[] references = node_text.Substring(47, 25).Trim().Split(",");

            List<int> references_list = new List<int>();
            for (int i = 0; i < references.Length; i++)
            {
                references_list.Add(Convert.ToInt32(references[i]));
            }

            //////////////////////////////////////////////////////////////////
            string values = node_text.Substring(73, ((Grade) * FieldLength));
            ////////////////////////////////////////////////////////////////////

            ///////////////////////////////////////////////////////////////////
            List<string> values_list = new List<string>();

            for (int i = 0; i < ((Grade) * FieldLength); i += FieldLength)
            {
                string current_value = values.Substring(i, FieldLength);
                if (current_value.Trim() != "") values_list.Add(current_value);
            }
            //////////////////////////////////////////////////////////////////////         

            List<T> valuesT = (List<T>)ValueConverter.DynamicInvoke(values_list);

            BNode<T> node = new BNode<T>(Grade - 1);
            node.Childs = references_list;
            node.Id = id;
            node.Father = father;
            node.Values = valuesT;

            return node;
        }

        public void WriteNode(BNode<T> node)
        {
            string str_node = node.ToString();
            string node_values = (string)ValueDeconverter.DynamicInvoke(node.Values, FieldLength);

            string format = "{0,-" + (FieldLength * Grade) + "}";
            str_node += $"{string.Format(format, node_values)}";
            byte[] bytes = Encoding.ASCII.GetBytes(str_node);
            int position = node.Id;
            byte newLine = 10;

            using (var fs = new FileStream(Path, FileMode.OpenOrCreate))
            {
                fs.Seek((position - 1) * LineLength + MetaDataLength, SeekOrigin.Begin);
                fs.WriteByte(newLine);
                fs.Write(bytes, 0, bytes.Length);
            }
        }


        public void DeleteFile()
        {
            string texto = "root:   0    \n" +
                           "next_id:1    \n" +
                           "grade:  0    \n" +
                           "\n" +
                           "id                  | father                  | childs                  | values \n";

            using var writer = new StreamWriter(Path, false);
            writer.Write(texto);
        }

    }
}
