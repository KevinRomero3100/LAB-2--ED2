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
        public FileManage<T> Fm;
        public int Count;

        public void IniciateTree()
        {
            int[] meta_data = Fm.ReadProperties();
            Root = meta_data[0];
            Next_Id = meta_data[1];
            Grade = meta_data[2];
            Fm.Grade = meta_data[2];
        }

        private BNode<T> GetNode(int position)
        {
            BNode<T> node = Fm.CastNode(position);
            if (node == null) node = new BNode<T>(Grade);

            return node;
        }

        public void Insert(T value)
        {
            BNode<T> root = GetNode(Root);

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
            List<T> currentList = new List<T>();

            BNode<T> root = GetNode(Root);
            if (root.Values != null)
            {
                PreOrden(root, currentList);
            }
            else
            {
                return null;
            }
            return currentList;
        }

        private void PreOrden(BNode<T> node, List<T> currentList)
        {
            TraverseNode(node.Values, currentList);
            for (int j = 0; j <= (Grade - 1); j++)
            {
                if (node.Childs[j] != -1)
                {
                    BNode<T> next_node = GetNode(node.Childs[j]);
                    PreOrden(next_node, currentList);
                }
            }
        }

        public List<T> ToInOrden()
        {
            List<T> currentList = new List<T>();

            BNode<T> root = GetNode(Root);
            if (root.Values != null)
            {
                InOrden(root, currentList);
            }
            else
            {
                return null;
            }
            return currentList;
        }

        private void InOrden(BNode<T> node, List<T> currentList)
        {
            for (int i = 0; i < node.Values.Count; i++)
            {
                if (i == 0)
                {
                    if (node.Childs[i] != -1)
                    {
                        BNode<T> next_node = GetNode(node.Childs[0]);
                        InOrden(next_node, currentList);
                    }
                    currentList.Add(node.Values[i]);
                    if (node.Childs[i + 1] != -1)
                    {
                        BNode<T> next_node = GetNode(node.Childs[i+1]);
                        InOrden(next_node, currentList);
                    }
                }
                else
                {
                    if (node.Values[i] != null)
                    {
                        currentList.Add(node.Values[i]);
                    }
                    if (node.Childs[i + 1] != -1)
                    {
                        BNode<T> next_node = GetNode(node.Childs[i+1]);
                        InOrden(next_node, currentList);
                    }
                }
            }
        }

        public List<T> ToPostOrden()
        {
            List<T> currentList = new List<T>();

            BNode<T> root = GetNode(Root);
            if (root.Values != null)
            {
                PostOrden(root, currentList);
            }
            else
            {
                return null;
            }
            return currentList;
        }

        private void PostOrden(BNode<T> node, List<T> currentList)
        {            
            for (int j = 0; j <= (Grade - 1); j++)
            {
                if (node.Childs[j] != -1)
                {
                    BNode<T> next_node = GetNode(node.Childs[j]);
                    PostOrden(next_node, currentList);
                }
            }
            TraverseNode(node.Values, currentList);
        }

        private void TraverseNode(List<T> value, List<T> currentList)
        {
            for (int i = 0; i < value.Count; i++)
            {
                currentList.Add(value[i]);
            }
        }
    }

}

