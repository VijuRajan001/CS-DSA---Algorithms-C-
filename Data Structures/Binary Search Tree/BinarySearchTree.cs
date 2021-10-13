using System;
using System.Collections;
using System.Collections.Generic;

namespace Binary_Search_Tree
{
    internal class Node<T>
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
            return Add(item,root);
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
