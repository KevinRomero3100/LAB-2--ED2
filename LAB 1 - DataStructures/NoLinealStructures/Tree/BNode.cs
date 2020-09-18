using System;
using System.Collections.Generic;
using System.Text;

namespace LAB_1___DataStructures.NoLinealStructures.Tree
{
    class BNode<T>
    {
        public int Id { get; set; }
        public int Father { get; set; }
        public List<int> Childs { get; set; }
        public List<T> Value { get; set; }

        public BNode(int grade, int id, int father, List<int> childs, List<T> value)
        {
            if (Value == null)
            {
                Value = new List<T>(grade - 1);
                Childs = new List<int>(grade);
                for (int i = 0; i < grade; i++)
                {
                    Childs.Add(-1);
                }
            }
           // Value.Add(value);
        }
    }
}
