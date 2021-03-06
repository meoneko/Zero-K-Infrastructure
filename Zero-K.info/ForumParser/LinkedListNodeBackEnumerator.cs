using System.Collections;
using System.Collections.Generic;

namespace ZeroKWeb.ForumParser
{
    public class LinkedListNodeBackEnumerator<T> : IEnumerator<LinkedListNode<T>>, IEnumerable<LinkedListNode<T>>
    {
        readonly LinkedListNode<T> startNode;
        LinkedListNode<T> node;

        public LinkedListNodeBackEnumerator(LinkedListNode<T> init)
        {
            startNode = init;
            node = init;
        }

        public IEnumerator<LinkedListNode<T>> GetEnumerator() => this;
        IEnumerator IEnumerable.GetEnumerator() => this;


        public void Dispose() { }

        public bool MoveNext()
        {
            if (Current.Previous != null)
            {
                node = node.Previous;
                return true;
            }
            return false;
        }

        public void Reset() => node = startNode;

        object IEnumerator.Current => node;
        public LinkedListNode<T> Current => node;
    }
}