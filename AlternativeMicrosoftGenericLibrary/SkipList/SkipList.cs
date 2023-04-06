using System;
using System.Collections;
using System.Collections.Generic;

namespace SkipList
{
    public class SkipList<TKey,TValue>:
        IEnumerable<KeyValuePair<TKey,TValue>> 
        where TKey :IComparable<TKey>
    {
        Node<TKey, TValue>[] _heads;
        //вероятностное значение для подьёма значения наверх
        readonly double _probability;

        readonly int _maxLevel;
        int _curLevel;

        //для подбрасывания монетки
        Random _random;
        public int Count { get; private set; }
        public SkipList(int maxLevel = 14, double p= 0.5)
        {
            _maxLevel = maxLevel;
            _probability = p;
            _heads = new Node<TKey, TValue>[_maxLevel];
            for (int i = 0; i < maxLevel; i++)
            {
                _heads[i] = new Node<TKey, TValue>();
                if (i != 0)
                {
                    _heads[i - 1].Up = _heads[i];
                    _heads[i].Down = _heads[i - 1];
                }             
            }

            _curLevel = 0;
            _random = new Random(DateTime.Now.Millisecond);
        }

        public void Add( TKey key, TValue value)
        {
            var prevNode = new Node<TKey, TValue>[_maxLevel];
            var currentNode = _heads[_curLevel];
            for (int i = _curLevel; i >= 0; i--)
            {
                while (currentNode.Right != null &&
                    currentNode.Right.Key.CompareTo(key) < 0)
                {
                    currentNode = currentNode.Right;
                }
                prevNode[i] = currentNode;
                if (currentNode.Down == null)
                    break;
                currentNode = currentNode.Down;
            }
            int level = 0;
            while (_random.NextDouble() < _probability && level < _maxLevel - 1)
            {
                level++;
            }
            while (_curLevel < level)
            {
                _curLevel++;
                prevNode[_curLevel] = _heads[_curLevel];
            }
            for (int i = 0; i <= level; i++)
            {
                var node = new Node<TKey, TValue>(key, value) { Right = prevNode[i].Right };
                prevNode[i].Right = node;
                if (i == 0) continue;
                node.Down = prevNode[i - 1].Right;
                prevNode[i - 1].Right.Up = node;
            }
            Count++;
        }

        public bool Contains(TKey key)
        {
            var searchedNode = Find(key, out Node<TKey, TValue>[] prevNodes);
            if (searchedNode != null && searchedNode.Key.CompareTo(key)==0) return true;
            return false;
        }

        public bool Remove(TKey key)
        {
            //первое вхождение
            var firstFounded = Find(key, out Node<TKey, TValue>[] prevNodes, isFindFirst: false);
            if (firstFounded == null) return false;

            //меняем ссылки на правый элемент
            var i = 0;
            while(firstFounded!= null)
            {
                prevNodes[i].Right = firstFounded.Right;
                firstFounded = firstFounded.Up;
                i++;
            }
            Count--;
            return true;
        }
        private Node<TKey,TValue> Find(TKey key,out Node<TKey, TValue>[] prevNodes, bool isFindFirst = true)
        {
            prevNodes = new Node<TKey, TValue>[_maxLevel];

            var currentNode = _heads[_curLevel];

            for (var i = _curLevel; i >= 0; i--)
            {
                //двигаемся по уровню максимально вправо
                while (currentNode.Right != null
                    && currentNode.Right.Key.CompareTo(key) < 0)
                {
                    currentNode = currentNode.Right;
                }

                //запоминаем самый элемент, с которого спускаемся дальше,
                //это нужно для того, чтобы потом справа поднимать вставляемый узел вверх, подбрасывая монету
                prevNodes[i] = currentNode;

                //оптимизация
                //если мы нашли его
                if (isFindFirst && currentNode.Right!= null && currentNode.Right.Key.CompareTo(key) == 0)
                {
                    break;
                }
                if(i != 0)currentNode = currentNode.Down;
            }
            return currentNode.Right;
        }
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            for(var node = _heads[0].Right; node.Right!=null; node=node.Right)
            {
                yield return new KeyValuePair<TKey,TValue>(node.Key, node.Value);
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
