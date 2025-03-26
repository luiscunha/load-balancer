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

        public bool IsEmpty() => _circularLinkedList.IsEmpty();
        
        public void AddResource(T resource)
        {                     
            _circularLinkedList.Add(resource);            
        }

        public bool Exists(T resource) => _circularLinkedList.Contains(resource);

        public T GetResource()
        {
            return _circularLinkedList.GetNext()!;
        }      
    }
}
