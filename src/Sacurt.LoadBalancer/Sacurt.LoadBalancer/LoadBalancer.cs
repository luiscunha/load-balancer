using Sacurt.Load_Balancer.Interfaces;

namespace Sacurt.Load_Balancer
{
    public class LoadBalancer<T>(ILoadBalancerStrategy<T> strategy) 
    {
        protected ILoadBalancerStrategy<T> Strategy { get; } = strategy;

        public async Task AddResourceAsync(T resource)
        {
            await Strategy.AddResourceAsync(resource); 
        }

        public async Task<T> GetResourceAsync()
        {
            return await Strategy.GetResourceAsync();
        }
    }
}
