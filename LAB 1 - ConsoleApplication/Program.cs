using System;
using System.Collections.Generic;
using System.IO;
using LAB_1___DataStructures.NoLinealStructures.Tree;
using LAB_1___DataStructures;

namespace LAB_1___ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\t\t\t- LAB 2 -\n\nKevin Romero 1047519\nJosé De León 1072619");

            string path = @"C:\Users\José De León\Desktop\lab\LAB-2--ED2\data.txt";


            BTree<int> Tree = new BTree<int>();
            Tree.IniciateTree(path, KeyComparison);



            Tree.Test(path, 5);



            Console.ReadLine();
        }

        public static Comparison<int> KeyComparison = delegate (int x1, int x2)
        {
            if (x1 > x2) return 1;
            if (x2 > x1) return -1;
            return 0;
        };
       
    }
}
