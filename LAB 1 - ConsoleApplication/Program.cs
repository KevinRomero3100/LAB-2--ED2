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
            FileManage<int> fm = new FileManage<int>();
            string fileName = "data.txt";
            string fullPath = Path.GetFullPath(fileName);
            string[] absolutePath = fullPath.Split("bin");
            fullPath = absolutePath[0] + fileName;
            if (!File.Exists(fullPath))
            {
                FileStream file = new FileStream(fullPath, FileMode.OpenOrCreate);
                file.Close();
            }

            
            fm.Path = fullPath;
            fm.DeleteFile();
            fm.UpdateGrade(grado);
            fm.FieldLength = longitud_campo;
            fm.LineLength = 75 + ((grado) * longitud_campo);
            fm.ValueConverter = ConvertNodetoT;
            fm.ValueDeconverter = ConvertTtoNode;

            BTree<int> Tree = new BTree<int>();
            Tree.Comparer = KeyComparison;
            Tree.Fm = fm;
            Tree.InitiateTree();

            ///PRUEBAS
            int[] values = new int[] { 15, 69, 34, 90,72,24,49,71,52,65,87,82,100,75,18,50,84,29,76,39,13,56,8,62,60,53,38,41,36,33,43,4,16};
            for (int i = 0; i < values.Length; i++)
            {
                Tree.Insert(values[i]);
            }

            bool resultado11 = Tree.Exist(90);
            bool resultado22= Tree.Exist(18);
            bool resultado33 = Tree.Exist(36);
            bool resultado44 = Tree.Exist(4);
            bool resultado55 = Tree.Exist(15);

            Tree.Delete(90);
            Tree.Delete(18);
            Tree.Delete(36);
            Tree.Delete(4);
            Tree.Delete(15);

            bool resultado1 = Tree.Exist(90);
            bool resultado2 = Tree.Exist(18);
            bool resultado3 = Tree.Exist(36);
            bool resultado4 = Tree.Exist(4);
            bool resultado5 = Tree.Exist(15);

            bool resultado6 = Tree.Exist(999);
            bool resultado7 = Tree.Exist(111);
            bool resultado8 = Tree.Exist(333);



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
