using System;
using System.Collections.Generic;

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


        public void Insert(T value)
        {
            if (Root == 0)
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
                        SplitParent(root);
                        return;
                    }
                    else
                        Fm.WriteNode(root);
                    return;
                }
            }
            else
            {
                Insert(root, value, 0);
                root = GetNode(root.Id);
                if (IsOverFlow(root)) SplitParent(root);
            }
        }

        private void Insert(BNode<T> root, T value, int pos)
        {
            if (IsLeaf(root))
            {
                if (!ExistInLeaf(root,value))
                {
                    IncertarEnHoja(root, value);
                    return;
                }
                return;
            }
            else if ((int)Comparer.DynamicInvoke(value, root.Values[pos]) == -1)
            {
                //incerto un hijo ala isquierda
                BNode<T> newRoot;
                newRoot = GetNode(root.Childs[pos]);
                Insert(newRoot, value, 0);
                newRoot = GetNode(root.Id);//leo de nuevo la roote para saber si existienron cambios
                if (IsOverFlow(newRoot)) SplitParent(newRoot);// si la root tubo cambios( y regreso onverfow divide esta root    
            }
            else if ((int)Comparer.DynamicInvoke(value, root.Values[pos]) == 1)
            {
                pos++;
                if (pos < root.Values.Count)
                {
                    Insert(root, value, pos);

                }
                else //incerta en el hijo mas ala derecha
                {
                    BNode<T> newRoot;
                    newRoot = GetNode(root.Childs[pos]);
                    Insert(newRoot, value, 0);

                    newRoot = GetNode(root.Id);
                    if (IsOverFlow(newRoot)) SplitParent(newRoot);
                }
            }
            else
            {
                return;
            }
        }

        #region Balanceos
        private void SplitParent(BNode<T> parentToSplit)
        {

            var middleValPos = (Grade - 1) / 2;
            var middleValue = parentToSplit.Values[middleValPos];

            BNode<T> parent;
            BNode<T> newParent = new BNode<T>(Grade);

            if (parentToSplit.Father == -1)
            {
                parent = new BNode<T>(Grade) { Father = -1, Id = SetNextId() };
                newParent.Father = parent.Id;
                newParent.Id = SetNextId();
                UpdateTree(parent.Id, Next_Id);
                parentToSplit.Father = parent.Id;
                
                newParent.Childs.Add(-1);

                parent.Childs.RemoveAt(0);
                parent.Childs.Insert(0, parentToSplit.Id);

                parent.Values.Add(middleValue);
                SortNode(parent);
            }
            else
            {
                parent = GetNode(parentToSplit.Father);

                newParent.Father = parentToSplit.Father;
                newParent.Id = SetNextId();

                newParent.Childs.Add(-1);

                parent.Values.Add(middleValue);
                SortNode(parent);
            }

            //Extraer valores mayores del nodo y los agrega ala nueva hoja
            for (int i = (middleValPos + 1); i < parentToSplit.Values.Count; i++)
            {
                newParent.Values.Add(parentToSplit.Values[i]);
            }
            // Quita los valores desde la mitad hasta el ultimo mayor
            while (middleValPos != parentToSplit.Values.Count)
            {
                parentToSplit.Values.RemoveAt(middleValPos);
            }
            // pasa  los hijos desde la mitad hasta el ultimo
            for (int i = 0; i < parentToSplit.Childs.Count; i++)
            {
                newParent.Childs.RemoveAt(i);
                newParent.Childs.Insert(i, parentToSplit.Childs[middleValPos + 1]);
                parentToSplit.Childs.RemoveAt(middleValPos + 1);
                parentToSplit.Childs.Add(-1);
            }
            BNode<T> tempNode;

            foreach (var item in newParent.Childs)
            {
                if (item != -1)
                {
                    tempNode = GetNode(item);
                    tempNode.Father = newParent.Id;
                    removeChild(tempNode);
                    Fm.WriteNode(tempNode);
                }
            }

            SortChilds(parent, newParent.Id, middleValue);

            removeChild(parentToSplit);
            removeChild(parent);
            removeChild(newParent);

            Fm.WriteNode(parentToSplit);
            Fm.WriteNode(parent);
            Fm.WriteNode(newParent);

        }
        private void IncertarEnHoja(BNode<T> leaf, T value)
        {
            BNode<T> parent;
            BNode<T> newLeaf = new BNode<T>(Grade);

            leaf.Values.Add(value);
            SortNode(leaf);

            // si hay bececidad de dividir la hoja
            if (IsOverFlow(leaf))
            {
                var middleValPos = (Grade - 1) / 2;
                var middleValue = leaf.Values[middleValPos];

                parent = GetNode(leaf.Father);
                parent.Values.Add(middleValue);
                parent = SortNode(parent);

                //Extraer valores mayores del nodo y los agrega ala nueva hoja
                for (int i = middleValPos + 1; i < leaf.Values.Count; i++)
                {
                    newLeaf.Values.Add(leaf.Values[i]);
                }
                // Quita los valores desde la mitad hasta el ultimo mayor
                while (middleValPos != leaf.Values.Count)
                {
                    leaf.Values.RemoveAt(middleValPos);
                }
                //Agregar Id y agreagr father
                newLeaf.Father = parent.Id;
                newLeaf.Id = SetNextId();

                SortNode(leaf);
                SortNode(newLeaf);

                SortChilds(parent, newLeaf.Id, middleValue);

                removeChild(parent);
                removeChild(leaf);
                removeChild(newLeaf);

                Fm.WriteNode(parent);
                Fm.WriteNode(leaf);
                Fm.WriteNode(newLeaf);
            }
            else
            {
                removeChild(leaf);
                Fm.WriteNode(leaf);
            }

        }
        #endregion 

        #region Auxiliares
        bool ExistInLeaf(BNode<T> leaf, T value)
        {
            foreach (var item in leaf.Values)
            {
                if ((int)Comparer.DynamicInvoke(value, item) == 0)
                    return true;
            }
            return false;
        }
        void removeChild (BNode<T> node)
        {
            if (node.Childs.Count > Grade && node.Childs[Grade] == -1)
            {
                node.Childs.RemoveAt(Grade);
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
        private void SortChilds(BNode<T> Parent, int IdChild, T value)
        {
            List<int> chilsSorted = new List<int>();
            var posVal = PositionValue(Parent, value);

            Parent.Childs.Insert(posVal , IdChild);
            if (Parent.Childs.Count == (Grade + 2))
            {
                Parent.Childs.RemoveAt(Grade+1);
            }
        }

        private int PositionValue(BNode<T> Parent, T value)
        {
            var count = 0;
            foreach (var item in Parent.Values)
            {
                if ((int)Comparer.DynamicInvoke(value, item) == 0)
                {
                    count++;
                    return count;
                }
                count++;
            }
            return count;
        }


        bool IsLeaf(BNode<T> node)
        {
            for (int i = 0; i < node.Childs.Count; i++)
            {
                if (node.Childs[i] != -1) return false;
            }
            return true;
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
        #endregion
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
        #region Funcionando
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
        #endregion
    }

}

