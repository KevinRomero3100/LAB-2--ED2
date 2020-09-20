using System;
using System.Collections.Generic;
using System.Text;

namespace LAB_1___DataStructures.NoLinealStructures.Tree
{
    public class BNode<T>
    {
        public int Id { get; set; }
        public int Father { get; set; }
        public List<int> Childs { get; set; } 
        public List<T> Values { get; set; }

        public BNode(T value, int grade)
        {

        }

        public BNode(int grade)
        {
            Values = new List<T>();
            Childs = new List<int>(grade);
            for (int i = 0; i < grade; i++)
            {
                Childs.Add(-1);
            }
        }

        public override string ToString()
        {
            string childs = "";
            for (int i = 0; i < Childs.Count; i++)
            {
                childs += Convert.ToString(Childs[i]) + ",";
            }
            childs = childs.Substring(0, childs.Length - 1);
            return $"{string.Format("{0,-10}", Id)}|{string.Format("{0,-10}", Father)}|{string.Format("{0,-30}", childs)}|";
        }

    }
}
