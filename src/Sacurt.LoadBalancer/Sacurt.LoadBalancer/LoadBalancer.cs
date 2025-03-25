using Sacurt.Load_Balancer.Interfaces;

namespace Sacurt.Load_Balancer
{
    public class LoadBalancer<T>(ILoadBalancerStrategy<T> strategy)
    {
        protected ILoadBalancerStrategy<T> Strategy { get; } = strategy;
        private readonly object _lock = new object();

        public void AddResource(T resource)
        {
            if (resource == null)
                throw new ArgumentNullException($"Cannot add null resource {nameof(resource)}.");

            lock (_lock)
            {
                Strategy.AddResource(resource);
            }
        }

        public T GetResource()
        {
            lock (_lock)
            {
                return Strategy.GetResource();
            }
        }
    }
}

