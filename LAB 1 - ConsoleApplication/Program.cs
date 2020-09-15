using System;
using System.Collections.Generic;

namespace LAB_1___ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\t\t\t- LAB 1 -\n\nKevin Romero 1047519\nJosé De León 1072619");


            LAB_1___DataStructures.NoLinealStructures.Tree.MultipathTree<int> Tree = new LAB_1___DataStructures.NoLinealStructures.Tree.MultipathTree<int>();
            Tree.Grade = 3;
            Tree.Comparer = KeyComparison;

            int[] ar = {62,85,93,43,25,28,36,52,5,63,38,94,81,33,69,40,20,88,97,16};
            string values = "";
            for (int i = 0; i < ar.Length; i++)
            {
                values += ar[i].ToString() + ", ";
                Tree.Insert(ar[i]);
            }

            List<int> TraversalTree;

            string inorden = "";
            TraversalTree = Tree.ToInOrden();
            for (int i = 0; i < TraversalTree.Count; i++)
            {
                inorden+=TraversalTree[i].ToString()+", ";
            }

            string preorden = "";
            TraversalTree = Tree.ToPreOrden();
            for (int i = 0; i < TraversalTree.Count; i++)
            {
                preorden += TraversalTree[i].ToString() + ", ";
            }


            string postorden = "";
            TraversalTree = Tree.ToPostOrden();
            for (int i = 0; i < TraversalTree.Count; i++)
            {
                postorden += TraversalTree[i].ToString() + ", ";
            }

            Console.WriteLine("\nValores: \n\t" + values.Substring(0, inorden.Length - 2));
            Console.WriteLine("\nGrado: \n\t"+Tree.Grade.ToString());


            Console.WriteLine("\n\n\nInorden: \n\t" + inorden.Substring(0,inorden.Length-2));
            Console.WriteLine("\nPreorden: \n\t" + preorden.Substring(0, inorden.Length - 2));
            Console.WriteLine("\nPostorden: \n\t" + postorden.Substring(0, inorden.Length - 2));

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
