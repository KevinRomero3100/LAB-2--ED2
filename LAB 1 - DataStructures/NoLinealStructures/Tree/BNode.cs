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

        public BNode(T value, int grade)
        {

        }

        public BNode(int id, int father, List<int> childs, List<T> value)
        {
            Value = value;
            Childs = childs;
            Father = father;
            Id = id;
        }

    }
}
