using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkipList2020
{
    public class SkipList<TKey,TValue>:
        IEnumerable<KeyValuePair<TKey,TValue>> 
        where TKey :IComparable<TKey>
    {
        Node<TKey, TValue>[] _head;
        readonly double _probability;
        readonly int _maxLevel;
        int _curLevel;
        Random _rd;
        public int Count { get; private set; }
        public SkipList(int maxLevel = 10, double p= 0.5)
        {
            _maxLevel = maxLevel;
            _probability = p;
            _head = new Node<TKey, TValue>[_maxLevel];
            for (int i = 0; i < maxLevel; i++)
            {
                _head[i] = new Node<TKey, TValue>();
                if(i!=0)
                {
                    _head[i].Down = _head[i - 1];
                    _head[i - 1].Up = _head[i];
                }
            }
            _curLevel = 0;
            _rd = new Random(DateTime.Now.Millisecond);
        }

        public void Add( TKey key, TValue value)
        {
            var prevNode = new Node<TKey, TValue>[_maxLevel];
            var currentNode = _head[_curLevel];
            for (int i= _curLevel; i>=0; i--)
            {
                while(currentNode.Right!=null && 
                    currentNode.Right.Key.CompareTo(key)<0)
                {
                    currentNode = currentNode.Right;
                }
                prevNode[i] = currentNode;
                if(i!=0)
                    currentNode = currentNode.Down;
            }
            int level = 0;
            while (_rd.NextDouble()< _probability && level<_maxLevel-1)
            {
                level++;
            }
            if(_curLevel<level)
            {
                do
                {   _curLevel++;
                    prevNode[_curLevel] = _head[_curLevel];
                } while (_curLevel < level);
            }
            for(int i=0; i<=level; i++)
            {
                var node = new Node<TKey, TValue>(key, value);
                node.Right = prevNode[i].Right;
                prevNode[i].Right = node;
                if(i!=0)
                {
                    node.Down = prevNode[i - 1].Right;
                    prevNode[i - 1].Right.Up = node;
                }
            }
            Count++;
        }

        private Node<TKey,TValue> Find(TKey key)
        {
            var currentNode = _head[_curLevel];
            for (int i = _curLevel; i >= 0; i--)
            {
                while (currentNode.Right != null &&
                    currentNode.Right.Key.CompareTo(key) < 0)
                {
                    currentNode = currentNode.Right;
                }
                if (currentNode.Right != null && currentNode.Right.Key.CompareTo(key) == 0)
                    return currentNode.Right;
                if (currentNode.Down == null)
                    return null;
                currentNode = currentNode.Down;
            }
            return null;
        }
        public bool Contains(TKey key)
        {
            if (Find(key) != null)
                return true;
            return false;
        }

        public bool Remove(TKey key)
        {
            var prevNode = new Node<TKey, TValue>[_maxLevel];
            var currentNode = _head[_curLevel];
            bool found = false;
            for (int i = _curLevel; i >= 0; i--)
            {
                while (currentNode.Right != null &&
                    currentNode.Right.Key.CompareTo(key) < 0)
                {
                    currentNode = currentNode.Right;
                }
                if(currentNode.Right != null && currentNode.Right.Key.CompareTo(key) == 0)
                {
                    prevNode[i] = currentNode;
                    found = true;
                }
                if (i != 0)
                    currentNode = currentNode.Down;
            }
            if (!found)
                return false;
            int j = 0;
            while(j<_maxLevel&&prevNode[j]!=null)
            {
                var node = prevNode[j].Right;
                prevNode[j].Right = node.Right;
                j++;
            }
            Count--;
            return true;
        }
        public TValue this[TKey key]
        {
            get
            {
                var node = Find(key);
                if (node == null)
                    throw new KeyNotFoundException();
                return node.Value;
            }
            set
            {
                var node = Find(key);
                if (node == null)
                    throw new KeyNotFoundException();
                node.Value = value;
            }
        }
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            for(var node = _head[0].Right; node.Right!=null; node=node.Right)
            {
                yield return new KeyValuePair<TKey,TValue>(node.Key, node.Value);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            for (var node = _head[0].Right; node.Right != null; node = node.Right)
            {
                yield return new KeyValuePair<TKey, TValue>(node.Key, node.Value);
            }
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            for (int i = _curLevel; i >= 0; i--)
            {
                var currentNode = _head[i];
                while (currentNode.Right != null)
                {
                    currentNode = currentNode.Right;
                    result.Append(currentNode.Key+" ");
                }

                if(i!=0)
                    currentNode = currentNode.Down;
                result.AppendLine();
            }
            return result.ToString();
        }
    }
}
