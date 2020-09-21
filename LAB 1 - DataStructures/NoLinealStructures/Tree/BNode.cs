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

        public BNode(int grade)
        {
            if (Values == null)
            {
                Values = new List<T>(grade - 1);
                Childs = new List<int>(grade);
                for (int i = 0; i < grade; i++)
                {
                    Childs.Add(-1);
                }
            }
        }

        public override string ToString()
        {
            string childs = "";
            for (int i = 0; i < Childs.Count; i++)
            {
                childs += Convert.ToString(Childs[i]) + ", ";
            }
            childs = childs.Substring(0, childs.Length - 2);
            return $"{string.Format("{0,-20}", Id)}|  {string.Format("{0,-23}", Father)}|  {string.Format("{0,-23}", childs)}|";
        }
    }
}
