using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Sacurt.Load_Balancer.Interfaces;
using Sacurt.LoadBalancer.Common;

namespace Sacurt.Load_Balancer.Implementations
{
    public class RoundRobinLoadBalancerStrategy<T> : ILoadBalancerStrategy<T> 
    {
        private Node<T>? _current;
        private Node<T>? _tail;

        public Task AddResourceAsync(T resource)
        {
            if (resource == null)
                throw new ArgumentNullException($"Cannot add null resource {nameof(resource)}.");

            var node = new Node<T>(resource);

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

            return Task.CompletedTask;
        }

        public Task<T> GetResourceAsync()
        {
            if(_current == null)
                throw new InvalidOperationException("No resources available.");

            var resource = _current!.Value;
            _current = _current.Next;
            return Task.FromResult(resource);

        }

    }
}
