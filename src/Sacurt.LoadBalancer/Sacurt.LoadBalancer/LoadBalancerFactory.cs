using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sacurt.Load_Balancer;
using Sacurt.Load_Balancer.Implementations;
using Sacurt.Load_Balancer.Interfaces;
using Sacurt.LoadBalancer.Implementations;

namespace Sacurt.Load_Balancer
{
    public class LoadBalancerFactory
    {
        private LoadBalancerFactory(){ }

        public static LoadBalancer<T> DefaultStrategy<T>() => RoundRobinStrategy<T>();

        public static LoadBalancer<T> RoundRobinStrategy<T>() => new LoadBalancer<T>(new RoundRobinLoadBalancerStrategy<T>());

        public static LoadBalancer<T> RandomStrategy<T>() => RandomStrategy<T>(Random.Shared);

        public static LoadBalancer<T> RandomStrategy<T>(Random random) => new LoadBalancer<T>(new RandomLoadBalancerStrategy<T>(random));

        public static LoadBalancer<T> WithStrategy<T>(ILoadBalancerStrategy<T> strategy) => new LoadBalancer<T>(strategy);
    }
}
