using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Sacurt.Load_Balancer.Interfaces;
using Sacurt.Load_Balancer.Common;

namespace Sacurt.Load_Balancer.Implementations
{
    public class RoundRobinLoadBalancerStrategy<T> : ILoadBalancerStrategy<T>
    {
        private CircularLinkedList<T> _circularLinkedList = new CircularLinkedList<T>();

        public void AddResource(T resource)
        {          
            if (_circularLinkedList.Contains(resource))
                throw new InvalidOperationException("Cannot add duplicated resource.");

            _circularLinkedList.Add(resource);            
        }

        public T GetResource()
        {
            if (_circularLinkedList.IsEmpty())
                throw new InvalidOperationException("No resources available.");

            return _circularLinkedList.GetNext()!;
        }
    }
}
