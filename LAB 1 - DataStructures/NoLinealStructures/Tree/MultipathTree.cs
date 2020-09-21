using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace LAB_1___DataStructures.NoLinealStructures.Tree
{
    public class MultipathTree<T> 
    {
        private Node<T> Root { get; set; }
        public int Grade;
        public Delegate Comparer;
        public int Count;
        public void Insert(T value)
        {
            if (Root == null)
            {
                Node<T> node = new Node<T>(value, Grade);
                Root = node;
                Count++;
            }
            else
            {
                Insert(Root, value);
            }
        }

        private void Insert(Node<T> nodef, T value)
        {
            if (!IsOverflow(nodef))
            {
                nodef.Value.Add(value);
                SortNode(nodef);
                Count++;
                return;
            }
            if ((int)Comparer.DynamicInvoke(nodef.Value[0], value) == 1)
            {
                if (nodef.References[0] == null)
                {
                    Node<T> node = new Node<T>(value, Grade);
                    nodef.References[0] = node;
                    Count++;
                    return;
                }
                Insert(nodef.References[0], value);
            }
            if ((int)Comparer.DynamicInvoke(nodef.Value[Grade - 2], value) == -1)
            {
                if (nodef.References[Grade - 1] == null)
                {
                    Node<T> node = new Node<T>(value, Grade);
                    nodef.References[Grade - 1] = node;
                    Count++;
                    return;
                }
                Insert(nodef.References[Grade - 1], value);
            }
            else
            {
                for (int i = 0; i < Grade - 2; i++)
                {
                    if ((int)Comparer.DynamicInvoke(value, nodef.Value[i]) == 1 && (int)Comparer.DynamicInvoke(value, nodef.Value[i + 1]) == -1)
                    {
                        if (nodef.References[i + 1] == null)
                        {
                            Node<T> node = new Node<T>(value, Grade);
                            nodef.References[i + 1] = node;
                            Count++;
                            return;
                        }
                        Insert(nodef.References[i + 1], value);
                    }
                }
            }
        }

        private bool IsOverflow(Node<T> node)
        {
            if (node.Value.Count < Grade - 1) return false;
            return true;
        }

        private Node<T> SortNode(Node<T> node)
        {
            int length = node.Value.Count;
            for (int i = 0; i < length - 1; i++)
            {
                for (int j = 0; j < length - i - 1; j++)
                {
                    if ((int)Comparer.DynamicInvoke(node.Value[j], node.Value[j + 1]) == 1)
                    {
                        T current_value = node.Value[j];
                        node.Value[j] = node.Value[j + 1];
                        node.Value[j + 1] = current_value;
                    }
                }
            }
            return node;
        }
        public List<T> ToPreOrden()
        {
            List<T> currentList = new List<T>();

            if (Root.Value != null)
            {
                PreOrden(Root, currentList);
            }
            else
            {
                return null;
            }
            return currentList;
        }

        private void PreOrden(Node<T> node, List<T> currentList)
        {
            TraverseNode(node.Value, currentList);
            for (int j = 0; j <= (Grade - 1); j++)
            {
                if (node.References[j] != null)
                {
                    PreOrden(node.References[j], currentList);
                }
            }
            return;
        } 

        public List<T> ToInOrden()
        {
            List<T> currentList = new List<T>();

            if (Root.Value != null)
            {
                InOrden(Root, currentList);
            }
            else
            {
                return null;
            }
            return currentList;
        }

        private void InOrden(Node<T> node, List<T> currentList)
        {
            for (int i = 0; i < node.Value.Count; i++)
            {
                if (i == 0)
                {
                    if (node.References[i] != null)
                    {
                        InOrden(node.References[0], currentList);
                    }
                    currentList.Add(node.Value[i]);
                    if (node.References[i + 1] != null)
                    {
                        InOrden(node.References[i + 1], currentList);
                    }
                }
                else
                {
                    if (node.Value[i] != null)
                    {
                        currentList.Add(node.Value[i]);
                    }
                    if (node.References[i + 1] != null)
                    {
                        InOrden(node.References[i + 1], currentList);
                    }
                }
            }
        }

        public List<T> ToPostOrden()
        {
            List<T> currentList = new List<T>();

            if (Root.Value != null)
            {
                PostOrden(Root, currentList);
            }
            else
            {
                return null;
            }
            return currentList;
        }
        private void PostOrden(Node<T> node, List<T> currentList)
        {
            for (int j = 0; j <= (Grade - 1); j++)
            {
                if (node.References[j] != null)
                {
                    PostOrden(node.References[j], currentList);
                }
            }
            TraverseNode(node.Value, currentList);
            return;
        }

        private void TraverseNode(List<T> value, List<T> currentList)
        {
            for (int i = 0; i < value.Count; i++)
            {
                currentList.Add(value[i]);
            }
        }

        public void Delete(T value)
        {
            throw new NotImplementedException();
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }

    }
}
