using System;
using System.Collections.Generic;
<<<<<<< HEAD
=======
using System.Dynamic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.X86;
using System.Xml;
>>>>>>> TreeDeleteMethod

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

<<<<<<< HEAD

=======
        public void Delete(T value)
        {
            BNode<T> root = GetNode(Root);
            if (IsLeaf(root))
            {
                if (root.Values.Contains(value))
                {
                    var posValues = root.Values.IndexOf(value);
                    root.Values.RemoveAt(posValues);
                    Fm.WriteNode(root);
                    return;
                }
                return;
            }
            else
            {
                Delete(root, value, 0, 0);
                root = GetNode(root.Id);
                if (IsUnderflow(root)) Balance(root);
            }
        }

        public void Delete( BNode<T> root,T value, int pos, int idParent)
        {

            if (root.Values.Contains(value))
            {
                var posValues = root.Values.IndexOf(value);
                if (IsLeaf(root))
                {
                    root.Values.RemoveAt(posValues);
                    if (IsUnderflow(root)) Balance(root);
                    else Fm.WriteNode(root);
                    return;
                }
                else
                {
                    var newRoot = GetNode(root.Childs[posValues]);
                    Delete(newRoot, value, 0, root.Id);

                    newRoot = GetNode(root.Id);
                    if (IsUnderflow(newRoot)) Balance(newRoot);
                    return;
                }
            }
            else if (!IsLeaf(root))
            {
                if ((int)Comparer.DynamicInvoke(value, root.Values[pos]) == -1)
                {
                    BNode<T> newRoot;
                    newRoot = GetNode(root.Childs[pos]);
                    Delete(newRoot, value, 0, idParent);

                    newRoot = GetNode(root.Id);
                    if (IsUnderflow(newRoot)) Balance(newRoot); 
                }
                else if ((int)Comparer.DynamicInvoke(value, root.Values[pos]) == 1)
                {
                    pos++;
                    if (pos < root.Values.Count)
                    {
                        Delete(root, value, pos, idParent);
                    }
                    else 
                    {
                        BNode<T> newRoot;
                        newRoot = GetNode(root.Childs[pos]);
                        Delete(newRoot, value, 0, idParent);

                        newRoot = GetNode(root.Id);
                        if (IsUnderflow(newRoot)) Balance(newRoot);
                    }
                }
                return;
            }
            else
            {
                if ((int)Comparer.DynamicInvoke(value, root.Values[root.Values.Count-1]) == 1)
                {
                    var valueSuplit = root.Values[root.Values.Count - 1];//obtengo ultimo valor
                    root.Values.Remove(valueSuplit);//quito de la hoja
                    var changeInParent = GetNode(idParent);//llamo al padre donde sustituire
                    var posDel = changeInParent.Values.IndexOf(value);//index del valor eliminado
                    changeInParent.Values.RemoveAt(posDel);//elimino de la raiz sonde se quedo contenido
                    changeInParent.Values.Insert(posDel, valueSuplit);//incerto el valor en la raiz donde lo encontre
                    Fm.WriteNode(changeInParent);

                    if (IsUnderflow(root))
                    {
                        root.Father = changeInParent.Id;
                        Balance(root);
                    };

                    return;
                }
                return; //caso en el que no se encontro el valor eliminado
            }
            
        }
        #region Unir Nodos
        void PutTogether(BNode<T> parent, BNode<T> reciveNode, BNode<T> addNode, int indexCommoRoot)
        {
            var commonRoot = parent.Values[indexCommoRoot];
            reciveNode.Values.Add(commonRoot);
            AddValues(reciveNode, addNode);
            int saveId = addNode.Id;
            

            if (addNode.Values.Count == 0)
            {
                if (!IsLeaf(addNode))
                    AddChids(reciveNode, addNode);
            }
            else if (!IsLeaf(addNode))
                AddChids(reciveNode, addNode);

            parent.Childs.Remove(addNode.Id);
            parent.Childs.Add(-1);
            parent.Values.RemoveAt(indexCommoRoot);

            addNode = new BNode<T>(Grade) { Father = -2, Id = saveId };

            Fm.WriteNode(parent);
            Fm.WriteNode(reciveNode); //nodo modificado con raiz en comun y resto de hojos
            Fm.WriteNode(addNode); //nodo eliminado

        }

        void AddValues(BNode<T> reciveNode, BNode<T> addNode)
        {
            foreach (var item in addNode.Values)
            {
                reciveNode.Values.Add(item);
            }
            
        }
        void AddChids(BNode<T> reciveNode, BNode<T> addNode)
        {
            var index = reciveNode.Childs.IndexOf(-1);
            for (int i = 0; i < addNode.Childs.Count; i++)
            {
                var item = addNode.Childs[0];
                if (item != -1)
                {
                    reciveNode.Childs.Insert(index,item);
                    addNode.Childs.Remove(item);
                    addNode.Childs.Add(-1);
                    reciveNode.Childs.Remove(-1);
                    index++;
                }
                
            }
        }
        #endregion
        public void Balance(BNode<T> nodeDef)
        {
            var parent = GetNode(nodeDef.Father);
            var indexChild = parent.Childs.IndexOf(nodeDef.Id);
            var whereHaveBrothers = WhereHaveBrothers(parent, indexChild);
            if (whereHaveBrothers == 0)
            {
                var rhigthBrother = GetNode(parent.Childs[indexChild + 1]);
                var leftBrother = GetNode(parent.Childs[indexChild - 1]);
                if (rhigthBrother.Values.Count > leftBrother.Values.Count)
                    GetRight(parent, rhigthBrother, nodeDef, parent.Values.IndexOf(parent.Values[indexChild]));
                else if (leftBrother.Values.Count > rhigthBrother.Values.Count) 
                    GetLeft(parent,leftBrother, nodeDef, parent.Values.IndexOf(parent.Values[indexChild - 1]));
                else
                {
                    if (CanLend(leftBrother))
                        GetLeft(parent, leftBrother, nodeDef, parent.Values.IndexOf(parent.Values[indexChild - 1]));
                    else
                        PutTogether(parent, leftBrother, nodeDef, parent.Values.IndexOf(parent.Values[indexChild-1]));
                }
            }
            else
            {
                if (whereHaveBrothers == 1)
                {
                    var rhigthBrother = GetNode(parent.Childs[indexChild + 1]);
                    if (CanLend(rhigthBrother))
                        GetRight(parent, rhigthBrother, nodeDef, parent.Values.IndexOf(parent.Values[indexChild]));
                    else
                        PutTogether(parent, nodeDef, rhigthBrother, parent.Values.IndexOf(parent.Values[indexChild]));
                }
                else if (whereHaveBrothers == -1)
                {
                    var leftBrother = GetNode(parent.Childs[indexChild - 1]);

                    if (CanLend(leftBrother))
                        GetLeft(parent, leftBrother, nodeDef, parent.Values.IndexOf(parent.Values[indexChild - 1]));
                    else
                        PutTogether(parent, leftBrother, nodeDef, parent.Values.IndexOf(parent.Values[indexChild - 1]));
                }
            }
        }//reglas del balanceo

        void GetRight(BNode<T> parent, BNode<T> rigthBrother, BNode<T> nodeDef, int indexCommonRoot)
        {
            //extraer valores que se van a prestar
            var commonRoot = parent.Values[indexCommonRoot];
            var newRoot = rigthBrother.Values[0];

            var nodeTransfered = new BNode<T>(Grade);
            if (!IsLeaf(nodeDef))
            {
                int indexTemp = nodeDef.Childs.IndexOf(-1);
                nodeTransfered = GetNode(rigthBrother.Childs[0]);
                nodeTransfered.Father = nodeDef.Id;
                nodeDef.Childs.Insert(indexTemp, nodeTransfered.Id);
                nodeDef.Childs.Remove(-1);
                rigthBrother.Childs.Remove(nodeTransfered.Id);
                rigthBrother.Childs.Add(-1);
                rigthBrother.Values.Remove(newRoot);
            }
            else
            {
                rigthBrother.Childs.RemoveAt(0);
                rigthBrother.Childs.Add(-1);
                rigthBrother.Values.Remove(newRoot);
            }

            //cabio valores
            nodeDef.Values.Add(commonRoot);
            parent.Values.Remove(commonRoot);
            parent.Values.Insert(indexCommonRoot, newRoot);

            Fm.WriteNode(nodeDef);
            if (nodeTransfered.Id != 0) 
            Fm.WriteNode(nodeTransfered);
            Fm.WriteNode(parent);
            Fm.WriteNode(rigthBrother);


        }
        void GetLeft(BNode<T> parent, BNode<T> leftBrother, BNode<T> nodeDef, int indexCommonRoot)
        {
            int index = 0;

            nodeDef.Values.Add(parent.Values[indexCommonRoot]);
            parent.Values[indexCommonRoot] = leftBrother.Values[leftBrother.Values.Count - 1];
            leftBrother.Values.RemoveAt(leftBrother.Values.Count - 1);

            if (leftBrother.Childs.Contains(-1))
            {
                index = leftBrother.Childs.IndexOf(-1);
                index -= 1;
            }
            else
            {
                index = leftBrother.Childs.Count - 1;
            }
            
            var nodeTransfered = GetNode(leftBrother.Childs[index]);
            nodeTransfered.Father = nodeDef.Id;

            leftBrother.Childs.Remove(nodeTransfered.Id);
            leftBrother.Childs.Add(-1);

            nodeDef.Childs.Insert(0, nodeTransfered.Id);
            nodeDef.Childs.Remove(-1);

            Fm.WriteNode(nodeDef);
            Fm.WriteNode(nodeTransfered);
            Fm.WriteNode(parent);
            Fm.WriteNode(leftBrother);
        }

        int WhereHaveBrothers(BNode<T> parent, int indexChild)
        {
            if (indexChild == 0)
                return 1;
            else if (indexChild == parent.Childs.Count - 1)
                return -1;
            else if (parent.Childs[indexChild + 1] == -1)
                return -1;
            else
            return 0;
        }
            
        bool IsUnderflow(BNode<T> bNode)
        {
            var min = (Grade -1)/ 2;
            if (bNode.Values.Count < min) return true;
            return false;
        }
        bool CanLend(BNode<T> bNode)
        {
            var minValues = (Grade-1) / 2;
            if (bNode.Values.Count == minValues) return false;
            return true;
        }
        


        #region Funciones Principales
>>>>>>> TreeDeleteMethod
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
<<<<<<< HEAD
=======
                        UpdateTree(Root, Next_Id);
>>>>>>> TreeDeleteMethod
                        return;
                    }
                    else
                        Fm.WriteNode(root);
<<<<<<< HEAD
=======
                    UpdateTree(Root, Next_Id);
>>>>>>> TreeDeleteMethod
                    return;
                }
            }
            else
            {
                Insert(root, value, 0);
                root = GetNode(root.Id);
                if (IsOverFlow(root)) SplitParent(root);
<<<<<<< HEAD
=======
                UpdateTree(Root, Next_Id);
>>>>>>> TreeDeleteMethod
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
<<<<<<< HEAD
=======
        #endregion
>>>>>>> TreeDeleteMethod

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
<<<<<<< HEAD
=======
       
>>>>>>> TreeDeleteMethod
        void removeChild (BNode<T> node)
        {
            if (node.Childs.Count > Grade && node.Childs[Grade] == -1)
            {
                node.Childs.RemoveAt(Grade);
            }
        }
<<<<<<< HEAD
=======
        
>>>>>>> TreeDeleteMethod
        int SetNextId()
        {
            Next_Id++;
            return Next_Id - 1;
        }
<<<<<<< HEAD
=======
        
>>>>>>> TreeDeleteMethod
        bool IsOverFlow(BNode<T> node)
        {
            if (node.Values.Count == Grade) return true;
            return false;
        }
<<<<<<< HEAD
=======
       
>>>>>>> TreeDeleteMethod
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
<<<<<<< HEAD

=======
        
>>>>>>> TreeDeleteMethod
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
<<<<<<< HEAD


=======
        
>>>>>>> TreeDeleteMethod
        bool IsLeaf(BNode<T> node)
        {
            for (int i = 0; i < node.Childs.Count; i++)
            {
                if (node.Childs[i] != -1) return false;
            }
            return true;
        }
<<<<<<< HEAD
=======

>>>>>>> TreeDeleteMethod
        private BNode<T> GetNode(int position)
        {
            BNode<T> node = Fm.CastNode(position);
            if (node == null) node = new BNode<T>(Grade);

            return node;
        }
<<<<<<< HEAD
=======

>>>>>>> TreeDeleteMethod
        private void UpdateTree(int root, int next_id)
        {
            Root = root;
            Fm.UpdateProperties(root, next_id);
        }
<<<<<<< HEAD
        #endregion
=======

>>>>>>> TreeDeleteMethod
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
<<<<<<< HEAD
        #region Funcionando
        public void Delete()
        {
            throw new NotImplementedException();
        }

        public void Delete(T value)
        {
            throw new NotImplementedException();
        }
=======
        #endregion

        #region Recorridos
>>>>>>> TreeDeleteMethod

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

