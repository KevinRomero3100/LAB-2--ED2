using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using LAB_1___DataStructures.NoLinealStructures.Tree;

namespace LAB_1___DataStructures.NoLinealStructures.Tree
{
    public class BTree<T> : Interfaces.ITreeDataStructure<T>
    {
        public int RootId { get; set; }
        public int Next_Id { get; set; }
        public int Grade { get; set; }

        public Delegate Comparer;
        public FileManage<T> fm;
        public int Count;

        public void IniciateTree(string path, Delegate comparer, Delegate converValues)
        {
            int[] meta_data = fm.ReadProperties(path);
            RootId = meta_data[0];
            Next_Id = meta_data[1];
            Grade = meta_data[2];
            Comparer = comparer;
            fm.Grade = meta_data[2];
        }

        private BNode<T> GetNode(int id)
        {
            return fm.CastNode(id);
        }

        public void Insert(T value)
        {
            BNode<T> Root = GetNode(RootId);
            if (Root == null)
            {
                BNode<T> newNode = new BNode<T>(Grade)
                {
                    Father = -1,
                    Id = SetNextId(),
                };
                newNode.Values.Add(value);
                fm.WriteNode(newNode);
            }
            else
            {
                if (!IsOverFlow(Root))
                {
                    Root.Values.Add(value);
                    SortNode(Root);
                    fm.WriteNode(Root);
                }
                else
                {
                    RootId++; //prueba aqui deveria ir el metodo recursivo;
                }
            }
            
        }


        void toIncert(BNode<T> nodef, T value, int id)
        {
        }
        private BNode<T> SortNode(BNode<T> node)
        {
            int length = node.Values.Count;
            for (int i = 0; i < length - 1; i++)
            {
                for (int j = 0; j < length - i - 1; j++)
                {
                    if ((int)Comparer.DynamicInvoke(node.Values[j], node.Values[j + 1]) == 1)
                    {
                        T current_value = node.Values[j];
                        node.Values[j] = node.Values[j + 1];
                        node.Values[j + 1] = current_value;
                    }
                }
            }
            return node;
        }
        int SetNextId()
        {
            Next_Id++;
            return Next_Id - 1;
        }
        bool IsOverFlow(BNode<T> node)
        {
            if (node.Values.Count == Grade) return true;
            return false;
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


    }

}

