using System;
using System.Collections.Generic;
using System.Text;
using LAB_1___DataStructures.NoLinealStructures.Tree;

namespace LAB_1___DataStructures.NoLinealStructures.Tree
{
    public class BTree<T> : Interfaces.ITreeDataStructure<T>
    {
        public int Root { get; set; }
        public int Next_Id { get; set; }
        public int Grade { get; set; }

        public Delegate Comparer;
        public int Count;

        public void IniciateTree(string path, Delegate comparer)
        {
            FileManage<T> fm = new FileManage<T>();
            int[] meta_data = fm.ReadProperties(path);
            Root = meta_data[0];
            Next_Id = meta_data[1];
            Grade = meta_data[2];
            Comparer = comparer;
        }

        private BNode<T> GetNode(string path, int id)
        {
            FileManage<T> fm = new FileManage<T>();
            return fm.CastNode(path, id, Grade);

        }

        public void Insert(T value)
        {
            throw new NotImplementedException();
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public void Delete(T value)
        {
            throw new NotImplementedException();
        }

        public List<T> ToPreOrden()
        {
            throw new NotImplementedException();
        }

        public List<T> ToInOrden()
        {
            throw new NotImplementedException();
        }

        public List<T> ToPostOrden()
        {
            throw new NotImplementedException();
        }

        public void Test(string path, int id)
        {
            BNode<T> test = GetNode(path, id);
            throw new NotImplementedException();
        }
    }

}

