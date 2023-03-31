using SokobanBruteforcer.Interfaces;

namespace SokobanBruteforcer
{
    public class IndexedQueue1<T> where T : IHashable
    {
        private T[] _queue;
        private Dictionary<string, int> _indices;

        public IndexedQueue1(int capacity)
        {
            _queue = new T[capacity];
            _indices = new Dictionary<string, int>();
        }

        public int Count { get { return _indices.Count; } }

        public void Enqueue(T item)
        {
            var hash = item.GetHash();
            if (_indices.ContainsKey(hash))
                throw new ArgumentException("Item already exists in queue");

            if (Count == _queue.Length)
                ResizeQueue(_queue.Length * 2);

            int index = Count;
            _queue[index] = item;
            _indices[hash] = index;
        }

        public T Dequeue()
        {
            if (Count == 0)
                throw new InvalidOperationException("Queue is empty");

            T item = _queue[0];
            _indices.Remove(item.GetHash());

            for (int i = 1; i < Count; i++)
            {
                _queue[i - 1] = _queue[i];
                _indices[_queue[i].GetHash()] = i - 1;
            }

            _queue[Count - 1] = default(T);

            if (Count <= _queue.Length / 4)
                ResizeQueue(_queue.Length / 2);

            return item;
        }

        public bool ContainsHash(string hash)
        {
            return _indices.ContainsKey(hash);
        }

        public T this[int index]
        {
            get { return _queue[index]; }
            set
            {
                if (!_indices.ContainsKey(value.GetHash()))
                    throw new ArgumentException("Item does not exist in queue");

                int oldIndex = _indices[value.GetHash()];
                if (oldIndex != index)
                {
                    _queue[oldIndex] = _queue[index];
                    _indices[_queue[index].GetHash()] = oldIndex;
                }

                _queue[index] = value;
                _indices[value.GetHash()] = index;
            }
        }

        private void ResizeQueue(int newSize)
        {
            T[] newQueue = new T[newSize];
            for (int i = 0; i < Count; i++)
            {
                newQueue[i] = _queue[i];
            }

            _queue = newQueue;
        }
    }

    public class IndexedQueue<T> where T : IHashable
    {
        private LinkedList<T> _queue;
        private Dictionary<string, LinkedListNode<T>> _indices;

        public IndexedQueue(int capacity)
        {
            _queue = new LinkedList<T>();
            _indices = new Dictionary<string, LinkedListNode<T>>(capacity);
        }

        public int Count { get { return _queue.Count; } }

        public T this[int index]
        {
            get { return _queue.ElementAt(index); }
        }

        public T this[string index]
        {
            get { return _indices[index].Value; }
        }

        public void Enqueue(T item)
        {
            string hash = item.GetHash();
            if (_indices.ContainsKey(hash))
                throw new ArgumentException("Item already exists in queue");

            LinkedListNode<T> node = _queue.AddLast(item);
            _indices[hash] = node;
        }

        public bool Any()
        {
            return _queue.Any();
        }
        
        public T Dequeue()
        {
            if (_queue.Count == 0)
                throw new InvalidOperationException("Queue is empty");

            T item = _queue.First.Value;
            _queue.RemoveFirst();
            _indices.Remove(item.GetHash());
            return item;
        }

        public bool ContainsHash(string hash)
        {
            return _indices.ContainsKey(hash);
        }

        public void RemoveByHash(string hash)
        {
            if (!_indices.ContainsKey(hash))
            {
                return;
                //throw new ArgumentException("Item does not exist in queue");
            }

            LinkedListNode<T> node = _indices[hash];

            _indices.Remove(hash);
            _queue.Remove(node);
        }
        
        public bool Contains(T item)
        {
            return _indices.ContainsKey(item.GetHash());
        }        

        //public T this[int index]
        //{
        //    get
        //    {
        //        if (index < 0 || index >= _queue.Count)
        //            throw new IndexOutOfRangeException();

        //        return _queue.ElementAt(index);
        //    }
        //    set
        //    {
        //        string oldHash = _queue.ElementAt(index).GetHash();
        //        string newHash = value.GetHash();

        //        if (!_indices.ContainsKey(oldHash))
        //            throw new ArgumentException("Item does not exist in queue");

        //        if (_indices.ContainsKey(newHash) && _indices[newHash].Value != value)
        //            throw new ArgumentException("Item already exists in queue");

        //        LinkedListNode<T> node = _indices[oldHash];
        //        _queue.Remove(node);
        //        _indices.Remove(oldHash);

        //        if (index == 0)
        //            _queue.AddFirst(value);
        //        else if (index == _queue.Count)
        //            _queue.AddLast(value);
        //        else
        //            _queue.AddBefore(_queue.ElementAt(index), value);

        //        _indices[newHash] = node;
        //    }
        //}
    }

}
