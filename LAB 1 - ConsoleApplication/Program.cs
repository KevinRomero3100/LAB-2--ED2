using System;
using System.Collections.Generic;
using System.IO;
using LAB_1___DataStructures.NoLinealStructures.Tree;
using LAB_1___DataStructures;
using Microsoft.VisualBasic;

namespace LAB_1___ConsoleApplication
{
    class Program
    {
        /// <summary>
        /// Que convierte el string leido en el values del archivo y convierte en la calse requerida, 
        /// se espera devuelva una lista de values.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        delegate List<int> ConvertValues(List<string> values);
        static ConvertValues convertToInt = new ConvertValues(ConvertirEnteros);

        delegate string ConvertString(List<int> values, int lend);
        static ConvertString convertTostring = new ConvertString(ConvertirString);



        static void Main(string[] args)
        {
            Console.WriteLine("\t\t\t- LAB 2 -\n\nKevin Romero 1047519\nJosé De León 1072619");
            FileManage<int> file = new FileManage<int>();
            
            string path = @"C:\Users\kevin\OneDrive\Documentos\GitHub\LAB-2--ED2\data.txt";
            file.Path = path;
            file.LineLength = 68;
            file.MetadataLength = 116;
            file.ConvertValues = convertToInt;
            file.GetValues = convertTostring;
            file.FieldLength = 3;


            BTree<int> Tree = new BTree<int>();
            Tree.fm = file;
            Tree.IniciateTree(path, KeyComparison, convertToInt);
            for (int i = 1; i < 51; i++)
            {
                Tree.Insert(i);
            }
            Console.ReadLine();
        }

        public static Comparison<int> KeyComparison = delegate (int x1, int x2)
        {
            if (x1 > x2) return 1;
            if (x2 > x1) return -1;
            return 0;
        };
        static List<int> ConvertirEnteros(List<string> values)
        {
            List<int> Values = new List<int>();
            foreach (var item in values)
            {
                if (int.Parse(item) != 0)
                {
                    Values.Add(int.Parse(item));
                }
            }
            return Values;
        }
        static string ConvertirString(List<int> values, int lend)
        {
            string Val = "";
            string lendS = "{0," + lend.ToString() + "}";
            foreach (var item in values)
            {
                Val = Val + $"{string.Format(lendS, item.ToString())}";
            }
            return Val;
        }
    }
}
