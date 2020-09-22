using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        private void UpdateTree(int root, int next_id)
        {
            Root = root;
            Fm.UpdateProperties(root, next_id);
        }

        public void Insert(T value)
        {
            if(Root == 0)
            {
                BNode<T> node = new BNode<T>(Grade)
                {
                    Father = -1,
                    Id = SetNextId(),
                };
                node.Values.Add(value);
                UpdateTree(node.Id, Next_Id);
                Fm.WriteNode(node);
                return;
            }
            BNode<T> root = GetNode(Root);
            if (IsLeaf(root))
            {
                if (!IsOverFlow(root))
                {
                    root.Values.Add(value);
                    SortNode(root);
                    if (IsOverFlow(root))
                    {
                        SimpleSplitRoot(root);
                        return;
                    }
                    Fm.WriteNode(root);
                    return;
                }
            }
            else
            {
                Insert(root, value);
            }         
        }

        private void Insert(BNode<T> root, T value)
        {
            if (IsLeaf(root))
            {
                root.Values.Add(value);
                return;
            }
            if((int)Comparer.DynamicInvoke(root.Values[0], value) == -1)
            {
                BNode<T> next_node = GetNode(root.Childs[0]);
                Insert(next_node, value);
            }
            if ((int)Comparer.DynamicInvoke(root.Values[root.Values.Count-1], value) == 1)
            {
                BNode<T> next_node = GetNode(root.Childs[root.Values.Count - 1]);
                Insert(next_node, value);
            }
            else
            {
                for (int i = 0; i < Grade - 2; i++)
                {
                    if ((int)Comparer.DynamicInvoke(value, root.Values[i]) == 1 && (int)Comparer.DynamicInvoke(value, root.Values[i + 1]) == -1)
                    {
                        BNode<T> next_node = GetNode(i+1);
                        Insert(next_node, value);
                    }
                }
            }

            //validar balanceos
        }

        void SimpleSplitRoot(BNode<T> node)
        {
            if(Grade%2 != 0)
            {
                int position = (Grade - 1) / 2;

                BNode<T> new_father = new BNode<T>(Grade) { Id = SetNextId(), Father = -1};
                T mid_value = node.Values[position];
                new_father.Values.Add(mid_value);

                BNode<T> right_node = new BNode<T>(Grade) { Id = SetNextId(), Father = new_father.Id};

                node.Values.RemoveAt(position);
                for (int i = 0; i < position; i++)
                {
                    right_node.Values.Add(node.Values[position]);
                    node.Values.RemoveAt(position);
                }

                new_father.Childs[0] = node.Id;
                new_father.Childs[1] = right_node.Id;

                UpdateTree(new_father.Id, Next_Id);
                Fm.WriteNode(node);
                Fm.WriteNode(new_father);
                Fm.WriteNode(right_node);

                return;
            }
            else
            {
                int position = (Grade - 1) / 2;

                BNode<T> new_father = new BNode<T>(Grade) { Id = SetNextId(), Father = -1 };
                T mid_value = node.Values[position];
                new_father.Values.Add(mid_value);

                BNode<T> right_node = new BNode<T>(Grade) { Id = SetNextId(), Father = new_father.Id };

                node.Values.RemoveAt(position);
                for (int i = position; i < position+1; i++)
                {
                    right_node.Values.Add(node.Values[i + 1]);
                    node.Values.RemoveAt(i);
                }

                new_father.Childs[0] = node.Id;
                new_father.Childs[1] = right_node.Id;
               
                return;
            }
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

        bool IsLeaf(BNode<T> node)
        {
            for (int i = 0; i < node.Childs.Count; i++)
            {
                if (node.Childs[i] != -1) return false;
            }
            return true;
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
                        BNode<T> next_node = GetNode(node.Childs[i + 1]);
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
                        BNode<T> next_node = GetNode(node.Childs[i + 1]);
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

