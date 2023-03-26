namespace SkipList
{
    internal class Node<TKey, TValue>
    {
        public TKey Key { get; set; }
        public TValue Value { get; set; }
        public Node<TKey, TValue> Right,
                            Up,
                            Down;
        public Node()
        { }
        public Node(TKey key, TValue value)
        {
            Key = key;
            Value = value;
            Right = null;
            Up = null;
            Down = null;
        }
        public override string ToString()
        {
            return $"Key:{Key}";
        }
    }
}
