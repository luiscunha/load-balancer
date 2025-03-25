using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sacurt.Load_Balancer;
using Sacurt.Load_Balancer.Implementations;
using Sacurt.Load_Balancer.Interfaces;

namespace Sacurt.Load_Balancer
{
    public class LoadBalancerFactory
    {
        private LoadBalancerFactory(){ }

        public static LoadBalancer<T> DefaultStrategy<T>() => RoundRobinStrategy<T>();

        public static LoadBalancer<T> RoundRobinStrategy<T>() => new LoadBalancer<T>(new RoundRobinLoadBalancerStrategy<T>());

        public static LoadBalancer<T> WithStrategy<T>(ILoadBalancerStrategy<T> strategy) => new LoadBalancer<T>(strategy);
    }
}
