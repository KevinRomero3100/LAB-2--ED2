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
        delegate List<int> ConvertValues(List<string> values);
        static ConvertValues ConvertNodetoT = new ConvertValues(ConvertToInteger);
        delegate string ConvertString(List<int> values, int lend);
        static ConvertString ConvertTtoNode = new ConvertString(ConvertToString);
        static void Main(string[] args)
        {
            Console.WriteLine("\t\t\t- LAB 2 -\n\nKevin Romero 1047519\nJosé De León 1072619");


            //VARIABLES//////////////////////////////////////////////////////////////
            int longitud_campo = 4;
            int grado = 6;
            /////////////////////////////////////////////////////////////////////////

            string fileName = "data.txt";
            string fullPath = Path.GetFullPath(fileName);

            FileManage<int> fm = new FileManage<int>();
            fm.Path = fullPath;
            fm.FieldLength = longitud_campo;
            fm.LineLength = 75 + ((grado) * longitud_campo);
            fm.ValueConverter = ConvertNodetoT;
            fm.ValueDeconverter = ConvertTtoNode;

            BTree<int> Tree = new BTree<int>();
            Tree.Comparer = KeyComparison;
            Tree.Fm = fm;
            Tree.IniciateTree();

            ///PRUEBAS

            Tree.Insert(15);
            Tree.Insert(84);
            Tree.Insert(69);
            Tree.Insert(34);
            Tree.Insert(90);
            Tree.Insert(72);
            Tree.Insert(24);
            Tree.Insert(100);
            Tree.Insert(49);
            Tree.Insert(71);
            Tree.Insert(52);
            Tree.Insert(65);
            Tree.Insert(87);
            Tree.Insert(82);
            Tree.Insert(84);
            Tree.Insert(100);
            Tree.Insert(76);
            Tree.Insert(41);
            Tree.Insert(75);
            Tree.Insert(18);
            Tree.Insert(50);
            Tree.Insert(84);
            Tree.Insert(29);
            Tree.Insert(24);
            Tree.Insert(76);
            Tree.Insert(39);
            Tree.Insert(13);
            Tree.Insert(56);
            Tree.Insert(8);
            Tree.Insert(62);
            Tree.Insert(75);
            Tree.Insert(60);
            Tree.Insert(53);
            Tree.Insert(38);
            Tree.Insert(41);
            Tree.Insert(36);
            Tree.Insert(33);
            Tree.Insert(43);
            Tree.Insert(4);
            Tree.Insert(16);



            //BNode<int> prueba = fm.CastNode(1);
            //prueba.Id = 2;
            //fm.WriteNode(prueba);
            //BNode<int> confirmacion = fm.CastNode(2);
            //BNode<int> nodetest = fm.CastNode(8);
            //nodetest.Id = 13;
            //nodetest.Father = 45;
            //fm.WriteNode(nodetest);


            ///


            //PREORDEN
            List<int> result = Tree.ToPreOrden();
            string result1 = "";
            for (int i = 0; i < result.Count; i++)
            {
                result1 += Convert.ToString(result[i]) + " ,";
            }
            Console.WriteLine("\nPreOrden \n" + result1);

            //POSTORDEN
            result = Tree.ToPostOrden();
            string result2 = "";
            for (int i = 0; i < result.Count; i++)
            {
                result2 += Convert.ToString(result[i]) + " ,";
            }
            Console.WriteLine("\nPostOrden \n" + result2);

            //INORDEN
            result = Tree.ToInOrden();
            string result3 = "";
            for (int i = 0; i < result.Count; i++)
            {
                result3 += Convert.ToString(result[i]) + " ,";
            }
            Console.WriteLine("\nInOrden \n" + result3);

            
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
            try
            {
                foreach (var item in values)
                {
                    if (int.Parse(item) != 0)
                    {
                        values_list.Add(int.Parse(item));
                    }
                }
                return values_list;
            }
            catch (Exception)
            {
                return values_list;
            }
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
