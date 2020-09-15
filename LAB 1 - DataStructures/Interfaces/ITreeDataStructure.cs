using LAB_1___DataStructures.NoLinealStructures.Tree;
using System;
using System.Collections.Generic;
using System.Text;

namespace LAB_1___DataStructures.Interfaces
{
    interface ITreeDataStructure<T>
    {
        void Insert(T value);
        void Delete();
        void Delete(T value);
        List<T> ToPreOrden();
        List<T> ToInOrden();
        List<T> ToPostOrden();
    }
}
