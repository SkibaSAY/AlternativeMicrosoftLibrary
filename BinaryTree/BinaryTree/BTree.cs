using System;
using System.Collections;
using System.Collections.Generic;

namespace BinaryTree
{
    public class BinaryTree<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>> where TKey : IComparable<TKey>
    {
        public int Count { get; private set; }
        private Node<TKey, TValue> _root; //корень
        public BinaryTree()
        {
            Count = 0;
        }
        public void Add(TKey key, TValue value)
        {
            var node = new Node<TKey, TValue>(key, value);
            if (_root == null)
            {
                _root = node;
                Count++;
                return;
            }
            var current = _root;
            var parent = _root;
            while (current != null)
            {
                parent = current;
                if (current.Key.CompareTo(node.Key) == 0)
                {
                    throw new ArgumentException("Such key is already added");
                }
                if (current.Key.CompareTo(node.Key) > 0)
                {
                    current = current.Left;
                }
                else if (current.Key.CompareTo(node.Key) < 0)
                {
                    current = current.Right;
                }
            }
            if (parent.Key.CompareTo(node.Key) > 0)
            {
                parent.Left = node;
            }
            if (parent.Key.CompareTo(node.Key) < 0)
            {
                parent.Right = node;
            }
            node.Parent = parent;
            Count++;
        }

        public bool Contains(TKey key, TValue value)
        {
            // Поиск узла осуществляется другим методом.
            return FindElem(key) != null;
        }
        private Node<TKey, TValue> FindElem(TKey findKey)
        {
            // Попробуем найти значение в дереве.
            var current = _root;

            // До тех пор, пока не нашли...
            while (current != null)
            {
                int result = current.Key.CompareTo(findKey);

                if (result > 0)
                {
                    // Если искомое значение меньше, идем налево.
                    current = current.Left;
                }
                else if (result < 0)
                {
                    // Если искомое значение больше, идем направо.
                    current = current.Right;
                }
                else
                {
                    // Если равны, то останавливаемся
                    break;
                }
            }

            return current;
        }

        

        private Node<TKey, TValue> FindMinElemOnTheRigthPath(Node<TKey, TValue> start)
        {
            while (start.Left != null)
            {
                start = start.Left;
            }
            return start;
        }

        

        IEnumerable<KeyValuePair<TKey, TValue>> Traverse(Node<TKey, TValue> node)
        {
            var nodes = new List<KeyValuePair<TKey, TValue>>();
            if (node != null)
            {
                nodes.AddRange(Traverse(node.Left));
                nodes.Add(new KeyValuePair<TKey, TValue>(node.Key, node.Value));
                nodes.AddRange(Traverse(node.Right));
            }
            return nodes;
        }
        public IEnumerable<KeyValuePair<TKey, TValue>> Traverse()
        {
            return Traverse(_root);
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Traverse().GetEnumerator();
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return Traverse().GetEnumerator();
        }
    }
}
