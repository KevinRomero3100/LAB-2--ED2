using System;
using System.Collections.Generic;

namespace LAB_1___ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\t\t\t- LAB 1 -\n\nKevin Romero 1047519\nJosé De León 1072619");


<<<<<<< Updated upstream
            LAB_1___DataStructures.NoLinealStructures.Tree.MultipathTree<int> Tree = new LAB_1___DataStructures.NoLinealStructures.Tree.MultipathTree<int>();
            Tree.Grade = 3;
            Tree.Comparer = KeyComparison;
=======
            //VARIABLES//////////////////////////////////////////////////////////////
            string path = @"C:\Users\kevin\OneDrive\Documentos\GitHub\LAB-2--ED2\data.txt";
            int longitud_campo = 4;
            int grado = 6;
            /////////////////////////////////////////////////////////////////////////

            ///////////////////////////////
            FileManage<int> fm = new FileManage<int>();
            fm.Path = path;
            fm.FieldLength = longitud_campo;
            fm.LineLength = 75 + ((grado) * longitud_campo);
            fm.Path = path;
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
            Tree.Insert(20);
            Tree.Insert(90);
            Tree.Insert(100);
            Tree.Insert(34);
            Tree.Insert(90);//
            Tree.Insert(72);
            Tree.Insert(24);
            Tree.Insert(100);//
            Tree.Insert(49);
            Tree.Insert(71);
            Tree.Insert(52);
            Tree.Insert(65);
            Tree.Insert(86);
            Tree.Insert(82);
            Tree.Insert(84);//
            Tree.Insert(100);
            Tree.Insert(76);
            Tree.Insert(41);
            Tree.Insert(75);
            Tree.Insert(18);
            Tree.Insert(50);
            Tree.Insert(84);//
            Tree.Insert(29);
            Tree.Insert(24);//
            Tree.Insert(76);//
            Tree.Insert(39);
            Tree.Insert(13);
            Tree.Insert(56);
            Tree.Insert(8);
            Tree.Insert(62);
            Tree.Insert(75);//
            Tree.Insert(60);
            Tree.Insert(53);
            Tree.Insert(38);
            Tree.Insert(41);//
            Tree.Insert(36);
            Tree.Insert(33);
            Tree.Insert(43);
            Tree.Insert(4);
            Tree.Insert(16);

            //INORDEN
            List<int> inorden = Tree.ToInOrden();
            string result3 = "";
            for (int i = 0; i < inorden.Count; i++)
            {
                result3 += Convert.ToString(inorden[i]) + " ,";
            }
            Console.WriteLine("\nInOrden \n" + result3);

            Tree.Delete(4);
            Tree.Delete(8);


            Tree.Delete(90);
            Tree.Delete(84);
            Tree.Delete(41);
            Tree.Delete(82);
            Tree.Delete(8);
            Tree.Delete(29);
            Tree.Delete(12);
            Tree.Delete(38);
            Tree.Delete(4);
            Tree.Delete(8);

>>>>>>> Stashed changes

            int[] ar = {62,85,93,43,25,28,36,52,5,63,38,94,81,33,69,40,20,88,97,16};
            string values = "";
            for (int i = 0; i < ar.Length; i++)
            {
                values += ar[i].ToString() + ", ";
                Tree.Insert(ar[i]);
            }

<<<<<<< Updated upstream
            List<int> TraversalTree;
=======
            //BNode<int> prueba = fm.CastNode(1);
            //prueba.Id = 2;
            //fm.WriteNode(prueba);
            //BNode<int> confirmacion = fm.CastNode(2);
            //BNode<int> nodetest = fm.CastNode(8);
            //nodetest.Id = 13;
            //nodetest.Father = 45;
            //fm.WriteNode(nodetest);


            ///

            //INORDEN
            inorden = Tree.ToInOrden();
            result3 = "";
            for (int i = 0; i < inorden.Count; i++)
            {
                result3 += Convert.ToString(inorden[i]) + " ,";
            }
            Console.WriteLine("\nInOrden \n" + result3);
>>>>>>> Stashed changes

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

<<<<<<< Updated upstream

            string postorden = "";
            TraversalTree = Tree.ToPostOrden();
            for (int i = 0; i < TraversalTree.Count; i++)
            {
                postorden += TraversalTree[i].ToString() + ", ";
            }
=======
            
>>>>>>> Stashed changes

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
