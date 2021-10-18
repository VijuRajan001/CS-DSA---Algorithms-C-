using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Binary_Search_Tree
{
    public class Node<T>
    {
        public T Item;
        public Node<T> Left { get; set; }
        public Node<T> Right { get; set; }

        public Node(T item)
        {
            // The default color will be red, we never need to create a black node directly.                
            this.Item = item;

        }

    }

    public class BinarySearchTreeRecursive<T> : ISet<T>, ICollection<T>, IReadOnlyCollection<T>
    {

        Node<T> root;
        IComparer<T> comparer;
        int count;
        int version;


        #region Constructors
        public BinarySearchTreeRecursive()
        {
            this.comparer = Comparer<T>.Default;

        }

        public BinarySearchTreeRecursive(IComparer<T> comparer)
        {
            if (comparer == null)
            {
                this.comparer = Comparer<T>.Default;
            }
            else
            {
                this.comparer = comparer;
            }
        }

        #endregion

        /*Count is an important factor in a binary tree*/
        public int Count => throw new NotImplementedException();

        public bool IsReadOnly => throw new NotImplementedException();

        /*<summary>
            Recursive implementation of Adding a node to Binary search Tree
            1. If root is null add to root and return
            2. If item value is less than current 
                If current.left is null add to current.left
                else recurssively call add on current.left
                        2. If item value is less than current 
            3. If item value is less than current 
                If current.right is null add to current.right
                else recurssively call add on current.right
        */

        internal bool Add(T item, Node<T> node)
        {
            var nodeToInsert = new Node<T>(item);

            if (this.root is null)
            {
                root = nodeToInsert;
                return true;
            }

            Node<T> current = root;
            var order = comparer.Compare(item, current.Item);

            if (order < 0)
            {
                if (current.Left is null)
                {
                    current.Left = nodeToInsert;
                    return true;
                }
                else
                    Add(item, current.Left);
            }
            else if (order > 0)
            {
                if (current.Right is null)
                {
                    current.Right = nodeToInsert;
                    return true;
                }
                else
                    Add(item, current.Right);
            }
            else
                return true;

            return false;
        }
        public virtual bool Add(T item)
        {
            return Add(item, root);
        }

        /*
            Remove All nodes of a binary tree
            Algorithms :
            Post Order Traversal 
                - Removes leaf nodes first followed by root nodes
        
        */
        public void Clear()
        {
            throw new NotImplementedException();
        }

        public void FindAll()
        {

        }

        public void FindAllInRange()
        {

        }

        internal Node<T> FindMinimum(Node<T> node)
        {
            while (node is not null)
            {
                node = node.Left;
            }

            return node;

        }

        internal Node<T> FindMaximum(Node<T> node)
        {
            while (node is not null)
            {
                node = node.Right;
            }

            return node;

        }
        internal Node<T> FindInOrderSuccessor(Node<T> node, T item)
        {

            if (node == null)
                return null;

            var order = comparer.Compare(item, node.Item);
            Node<T> successor = null;
            if (order == 0)
            {
                if (node.Right != null)
                    return FindMinimum(node.Right);
            }
            else if (order < 0)
            {
                successor = node;
                FindInOrderSuccessor(node.Left, item);
            }
            else if (order > 0)
                FindInOrderSuccessor(node.Right, item);


            return successor is not null ? successor : null;

        }

        public Node<T> InOrderSuccessor(T item)
        {
            return FindInOrderSuccessor(root, item);
        }


        internal Node<T> FindInOrderPredecessor(Node<T> node, T item)
        {

            if (node == null)
                return null;

            var order = comparer.Compare(item, node.Item);
            Node<T> predecessor = null;

            if (order == 0)
            {
                if (node.Left != null)
                    return FindMaximum(node.Left);
            }
            else if (order < 0)
            {

                FindInOrderSuccessor(node.Left, item);
            }
            else if (order > 0)
            {
                predecessor = node;
                FindInOrderSuccessor(node.Right, item);
            }

            return predecessor is not null ? predecessor : null;

        }
        public Node<T> InOrderPredecessor(T item)
        {
            return FindInOrderPredecessor(root,item);
        }

        internal virtual bool InOrder(Predicate<Node<T>> action, Node<T> node, bool reverse = false)
        {
            if (root == null)
                return false;

            InOrder(action, !reverse ? node.Left : node.Right, reverse);

            if (!action(node))
                return false;

            InOrder(action, !reverse ? node.Right : node.Left, reverse);

            return true;
        }

        internal virtual bool PreOrder(Predicate<Node<T>> action, Node<T> node)
        {
            if (node == null)
                return false;


            if (!action(node))
                return false;

            PreOrder(action, node);

            PreOrder(action, node);

            return true;

        }

        internal virtual Node<T> FindPreOrderSucessor(Node<T> node,T item,Stack<Node<T>> parents)
        {
            //25,15,10,4,12,22,18,24,50,35,31,44,70,66,90
            if (node == null)
                return null;
            var order = comparer.Compare(item,node.Item);
            
            if(order == 0)
            {

                if(node.Left is not null ) return node.Left;
                else if(node.Right is not null) return node.Right;
                else
                {
                    //node is a leaf node ans is is ancestor which has a right subtree
                    var current  = node;
                    var parent = parents.Any() ? parents.Peek() : null;;

                    while(parents.Count > 0 && parent.Right == current)
                    {
                        parent = parents.Pop();   
                         current = parent;
                    }

                // If we reached root, then the given
                // node has no preorder successor
                 if (parent == null)
                    return null;
 
                return parent.Right;

                }
            }
            else if(order < 0)
            {
                parents.Push(node);
                return FindPreOrderSucessor(node.Left,item,parents);
            }
            else
            {
                parents.Push(node);
                return FindPreOrderSucessor(node.Right,item,parents);
            }
            
        }

        public Node<T> PreOrderSucessor(T item)
        {
            Stack<Node<T>> stack = new Stack<Node<T>>();

            return FindPreOrderSucessor(root,item,stack);
        }


        internal virtual bool PostOrder(Predicate<Node<T>> action, Node<T> node)
        {
            if (node == null)
                return false;

            PostOrder(action, node);

            PostOrder(action, node);

            if (!action(node))
                return false;

            return true;

        }

        internal virtual Node<T> FindPostOrderSucessor(Node<T> node,T item,Node<T> parent)
        {
            //25,15,10,4,12,22,18,24,50,35,31,44,70,66,90
            if (node == null)
                return null;
            var order = comparer.Compare(item,node.Item);
            
            if(order == 0)
            {
                var isLeft = false;
                if(parent == null ) return null;

                if(parent.Left is null) return parent;

                if(parent.Right is null) return parent;

                
                // We found the element with both left and right nodes

                isLeft = comparer.Compare(item,parent.Left.Item) == 0 ? true : false;

                if(isLeft)
                    FindMinimum(parent.Right);

                return parent;

            }
            else if(order < 0)
            {
                parent =node;
                return FindPostOrderSucessor(node.Left,item,parent);
            }
            else
            {
                parent =node;
                return FindPostOrderSucessor(node.Right,item,parent);
            }
            
        }

        public Node<T> PostOrderSucessor(T item)
        {
            

            return FindPostOrderSucessor(root,item,null);
        }

        public void Validate()
        {

        }


        public void PathToLeafNode()
        {


        }



        internal Node<T> FindNode(Node<T> rootNode, T item)
        {
            if (item is null || root is null) return null;

            var current = rootNode;

            var order = comparer.Compare(root.Item, item);

            if (order == 0)
                return current;
            else if (order < 0)
                return FindNode(current.Left, item);
            else
            {
                return FindNode(current.Right, item);
            }
        }


        internal Node<T> FindNode(Node<T> rootNode, T other, Func<Node<T>, T, bool> match)
        {
            if (rootNode is null) return null;

            if (match == null) throw new ArgumentException("match");

            var current = rootNode;

            if (match(current, other)) return current;

            var order = comparer.Compare(other, current.Item);

            while (current != null)
            {
                if (order < 0)
                    return FindNode(current.Left, other, match);

                if (order > 0)
                    return FindNode(current.Right, other, match);


            }

            return null;


        }
        public virtual bool Contains(T item)
        {
            return FindNode(root, item) != null;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public void ExceptWith(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public void IntersectWith(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public bool IsProperSubsetOf(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public bool IsProperSupersetOf(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public bool IsSubsetOf(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public bool IsSupersetOf(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public bool Overlaps(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }


        internal bool HasSiblings(Node<T> node) => node.Left is not null && node.Right is not null;
        internal bool HasOneSiblings(Node<T> node) => (node.Left is not null && node.Right is null) || (node.Right is not null && node.Left is null);
        internal bool IsLeafNode(Node<T> node) => (node.Left is null && node.Right is null);

        /*
       Algorithm :
       Three Steps:

       1. Find the node by traversing 
           if node not found return null
           if found check for below 3 cases 
       2. if node has no children remove the node
       3. if node has one child replace parent.Item with child.Item remove child
       4. if node has two child
              Replace data with inorder sucessor(Left most element of right subtree)

           //You can also replace with inorder predecessor for removing node
       */
        internal Node<T> Remove(Node<T> node, T item)
        {

            if (node == null) return node;

            var order = comparer.Compare(item, node.Item);

            if (order < 0)
                node.Left = Remove(node.Left, item);
            else if (order > 0)
                node.Right = Remove(node.Right, item);
            else
            {
                if (IsLeafNode(node))
                    node = null;
                else if (HasSiblings(node))
                {
                    var current = node;
                    while (current.Left != null)
                        current = current.Left;

                    var sucessor = current;

                    node.Item = sucessor.Item;

                    node.Right = Remove(node.Right, sucessor.Item);
                }
                else
                {
                    var child = node.Left != null ? node.Left : node.Right;
                    node = child;
                }
            }

            return node;

        }
        public bool Remove(T item)
        {
            return Remove(root, item) != null;
        }

        public bool SetEquals(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public void SymmetricExceptWith(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public void UnionWith(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        void ICollection<T>.Add(T item)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }

    public class BinarySearchTree<T> : ISet<T>, ICollection<T>, IReadOnlyCollection<T>
    {
        public int Count => throw new NotImplementedException();

        public bool IsReadOnly => throw new NotImplementedException();

        public bool Add(T item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(T item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public void ExceptWith(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public void IntersectWith(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public bool IsProperSubsetOf(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public bool IsProperSupersetOf(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public bool IsSubsetOf(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public bool IsSupersetOf(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public bool Overlaps(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public bool Remove(T item)
        {
            throw new NotImplementedException();
        }

        public bool SetEquals(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public void SymmetricExceptWith(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public void UnionWith(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        void ICollection<T>.Add(T item)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }



}
