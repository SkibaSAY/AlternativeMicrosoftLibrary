using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace HashTableForStudents
{
    public class OpenAddressHashTable<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>, IHashTable<TKey, TValue> where TKey: IEquatable<TKey>
    {
        Pair<TKey, TValue>[] _table;
        private int _capacity;
        HashMaker<TKey> _hashMaker1, _hashMaker2;
        public int Count { get; private set; }
        private const double FillFactor = 0.70;
        private readonly GetPrimeNumber _primeNumber = new GetPrimeNumber();

        public OpenAddressHashTable() 
        {
            _capacity = _primeNumber.GetMin();
            _table = new Pair<TKey, TValue>[_capacity];
            _hashMaker1 = new HashMaker<TKey>(_capacity);
            _hashMaker2 = new HashMaker<TKey>(_capacity - 1);
            Count = 0;
        }
        public OpenAddressHashTable(int m)
        {
            _table = new Pair<TKey, TValue>[m];
            _capacity = m;
            _hashMaker1 = new HashMaker<TKey>(_capacity);
            _hashMaker2 = new HashMaker<TKey>(_capacity - 1);
            Count = 0;
        }
        public void Add(TKey key, TValue value)
        {
            var hash = _hashMaker1.ReturnHash(key);

            if (!TryToPut(hash, key, value)) // ячейка занята
            {
                int iterationNumber = 1;
                while (true) 
                {
                    var place = CalculateSquareHash(hash, iterationNumber) % _capacity;
                    if (TryToPut(place, key, value))
                        break;
                    iterationNumber++;
                    if (iterationNumber >= _capacity)
                        throw new ApplicationException("HashTable full!!!");
                }
            }
            if ((double) Count / _capacity >= FillFactor)
            {
                IncreaseTable();    
            }
        }

        private bool TryToPut(int place, TKey key, TValue value)
        {
            if (_table[place] == null || _table[place].IsDeleted())
            {
                _table[place] = new Pair<TKey, TValue>(key, value);
                Count++;
                return true;
            }
            if (_table[place].Key.Equals(key))
            {
                throw new ArgumentException();
            }
            return false;
        }

        //TODO: проверить, будет ли правильно работать
        private Pair<TKey,TValue> Find(TKey x)
        {
            var hash = _hashMaker1.ReturnHash(x);
            if (_table[hash] == null)
                return null;
            if (!_table[hash].IsDeleted() && _table[hash].Key.Equals(x))
            {
                return _table[hash];
            }
            int iterationNumber = 1;
            while (true)
            {
                var place = CalculateSquareHash(hash,iterationNumber) % _capacity;
                if (_table[place] == null)
                    return null;
                if (!_table[place].IsDeleted() && _table[place].Key.Equals(x))
                {
                    return _table[place];
                }
                iterationNumber++;
                if (iterationNumber >= _capacity)
                    return null;
            }
        }
        public TValue this[TKey key]
        {
            get
            {
                var pair = Find(key);
                if (pair == null)
                    throw new KeyNotFoundException();
                return pair.Value;
            }

            set
            {
                var pair = Find(key);
                if (pair== null)
                    throw new KeyNotFoundException();
                pair.Value = value;
            }
        }

        #region SquareHash
        private static int c1 = 1;
        private static int c2 = 1;
        private int CalculateSquareHash(int hash,int iterationNumber)
        {
            var squareHash = (hash + c1*iterationNumber +c2* iterationNumber* iterationNumber);
            return squareHash;
        }
        #endregion

        private void IncreaseTable()
        {
            //TODO: написать код
            int size = _primeNumber.Next();
            _hashMaker1.SimpleNumber = size;
            var tempTable = _table;
            _table = new Pair<TKey, TValue>[size];
            Count = 0;
            _capacity = size;
            foreach (var tableItem in tempTable)
            {
                if (tableItem == null) continue;
                Add(tableItem.Key, tableItem.Value);
            }
        }
        //TODO:написать
        public bool Remove(TKey key)
        {
            Pair<TKey, TValue> node = Find(key);
            if (node == null)
            {
                return false;
            }
            node.DeletePair();
            Count--;
            return true;
        }

        public bool ContainsKey(TKey key)
        {
            return Find(key) != null;
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return (from pair in _table where pair != null && !pair.IsDeleted() select new KeyValuePair<TKey, TValue>(pair.Key, pair.Value)).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
