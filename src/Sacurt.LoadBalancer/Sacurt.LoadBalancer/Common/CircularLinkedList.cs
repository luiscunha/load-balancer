using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sacurt.Load_Balancer.Common
{
    public class CircularLinkedList<T>
    {
        private Node<T>? _tail;
        private Node<T>? _current;

        public void Add(T value)
        {
            var node = new Node<T>(value);

            if (_tail == null)
            {
                _tail = node;
                _tail.Next = _tail;
                _current = node;

            }
            else
            {
                var tempNode = _tail.Next;
                _tail.Next = node;
                node.Next = tempNode;
                _tail = node;
            }

        }
        public bool IsEmpty()
        {
            return _current == null;
        }   

        public bool Contains(T value)
        {
            var node = _tail;

            if (node == null)
                return false;
             
            while(true)
            { 
                if (node.Value.Equals(value))
                   return true; 

                node = node.Next;

                if(node == _tail)
                    break;  
            }

            return false;
        }

        public T? GetNext()
        {
            if (IsEmpty())
                return default;

            var resource = _current!.Value;
            _current = _current.Next;
            return resource;
        }

    }
}
