using System;
using System.Collections.Generic;
using System.Text;

namespace LAB_1___DataStructures.NoLinealStructures.Tree
{
    class Node<T>
    {
        public List<Node<T>> References { get; set; }
        public List<T> Value { get; set; }

        public Node(T value, int grade)
        {
            if (Value == null)
            {
                Value = new List<T>(grade - 1);
                References = new List<Node<T>>(grade);
                for (int i = 0; i < grade; i++)
                {
                    References.Add(null);
                }
            }
            Value.Add(value);
        }
    }
}
