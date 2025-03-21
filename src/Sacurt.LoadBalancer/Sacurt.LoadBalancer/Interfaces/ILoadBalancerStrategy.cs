using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sacurt.Load_Balancer.Interfaces
{
    public interface ILoadBalancerStrategy<T> 
    {
        Task AddResourceAsync(T resource);

        Task<T> GetResourceAsync();
    }
}
