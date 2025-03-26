using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sacurt.Load_Balancer.Common;
using Sacurt.Load_Balancer.Interfaces;

namespace Sacurt.LoadBalancer.Implementations
{
    public class RandomLoadBalancerStrategy<T> : ILoadBalancerStrategy<T> 
    {
        private ConcurrentBag<T> _resources = new ConcurrentBag<T>();
        private readonly Random _random;

        public RandomLoadBalancerStrategy() : this(Random.Shared) { }

        public RandomLoadBalancerStrategy(Random random)
        {
            _random = random ?? Random.Shared;
        }
        public bool IsEmpty() => _resources.IsEmpty;

        public void AddResource(T resource)
        {
            _resources.Add(resource);
        }
        public bool Exists(T resource) => _resources.Any(r => r.Equals(resource));        

        public T GetResource()
        { 
            var random = _random.Next(0, _resources.Count);

            return _resources.ElementAt(random);
        }

    }
}
