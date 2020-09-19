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

        static void Main(string[] args)
        {
            Console.WriteLine("\t\t\t- LAB 2 -\n\nKevin Romero 1047519\nJosé De León 1072619");

            string path = @"C:\Users\kevin\OneDrive\Documentos\GitHub\LAB-2--ED2\data.txt";
            BTree<int> Tree = new BTree<int>();
            
            Tree.IniciateTree(path, KeyComparison, convertToInt);

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
            List<int> Values = new List<int>(5-1);
            foreach (var item in values)
            {
                Values.Add(int.Parse(item));
            }
            return Values;
        }
    }
}
