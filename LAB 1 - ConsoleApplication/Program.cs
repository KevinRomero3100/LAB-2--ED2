﻿using System;
using System.Collections.Generic;
using System.IO;
using LAB_1___DataStructures.NoLinealStructures.Tree;
using LAB_1___DataStructures;

namespace LAB_1___ConsoleApplication
{
    class Program
    {
        delegate List<int> ConvertValues(List<string> values);
        static ConvertValues ConvertNodetoT = new ConvertValues(ConvertToInteger);
        delegate string ConvertString(List<int> values, int lend);
        static ConvertString ConvertTtoNode = new ConvertString(ConvertToString);
        static void Main(string[] args)
        {
            Console.WriteLine("\t\t\t- LAB 2 -\n\nKevin Romero 1047519\nJosé De León 1072619");

            //VARIABLES//////////////////////////////////////////////////////////////
            string path = @"C:\Users\José De León\Desktop\lab\LAB-2--ED2\data.txt";
            int longitud_campo = 3;
            int grado = 5;
            /////////////////////////////////////////////////////////////////////////

            FileManage<int> fm = new FileManage<int>();
            fm.Path = path;
            fm.FieldLength = longitud_campo;
            fm.LineLength = 75 + ((grado-1)*longitud_campo);
            fm.Path = path;
            fm.ValueConverter = ConvertNodetoT;
            fm.ValueDeconverter = ConvertTtoNode;

            BTree<int> Tree = new BTree<int>();
            Tree.Comparer = KeyComparison;
            Tree.Fm = fm;
            Tree.IniciateTree();

            //PREORDEN
            List<int> preorden = Tree.ToPreOrden();
            string result1 ="";
            for (int i = 0; i < preorden.Count; i++)
            {
                result1 += Convert.ToString(preorden[i])+" ,";
            }
            Console.WriteLine("\nPreOrden \n"+result1);

            //POSTORDEN
            List<int> postorden = Tree.ToPostOrden();
            string result2 = "";
            for (int i = 0; i < postorden.Count; i++)
            {
                result2 += Convert.ToString(postorden[i]) + " ,";
            }
            Console.WriteLine("\nPostOrden \n" + result2);

            //INORDEN
            List<int> inorden = Tree.ToInOrden();
            string result3 = "";
            for (int i = 0; i < inorden.Count; i++)
            {
                result3 += Convert.ToString(inorden[i]) + " ,";
            }
            Console.WriteLine("\nInOrden \n" + result3);

            BNode<int> nodetest = fm.CastNode(8);
            nodetest.Id = 13;
            nodetest.Father = 45;


            fm.WriteNode(nodetest);


            Console.ReadLine();
        }

        public static Comparison<int> KeyComparison = delegate (int x1, int x2)
        {
            if (x1 > x2) return 1;
            if (x2 > x1) return -1;
            return 0;
        };

        static List<int> ConvertToInteger(List<string> values)
        {
            List<int> values_list = new List<int>();
            foreach (var item in values)
            {
                if (int.Parse(item) != 0)
                {
                    values_list.Add(int.Parse(item));
                }
            }
            return values_list;
        }

        static string ConvertToString(List<int> values, int lend)
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
