using Sacurt.Load_Balancer;
using Sacurt.Load_Balancer.Implementations;

namespace Sacurt.LoadBalancer.Tests
{
    [TestClass]
    public class RoundRobinLoadBalancerTests
    {
        [TestMethod]
        public async Task RoundRobin_Algorithm_ShouldReturnInitialValueAfterOneRound()
        {
            // Arrange
            var server1 = "server1";
            var server2 = "server2";
            var server3 = "server3";

            var roundRobinStrategy = new RoundRobinLoadBalancerStrategy<string>();
            var loadBalancer = new LoadBalancer<string>(roundRobinStrategy);

            // Act
            await loadBalancer.AddResourceAsync(server1);
            await loadBalancer.AddResourceAsync(server2);
            await loadBalancer.AddResourceAsync(server3);

            var resource1 = await loadBalancer.GetResourceAsync();
            var resource2 = await loadBalancer.GetResourceAsync();
            var resource3 = await loadBalancer.GetResourceAsync();
            var resource4 = await loadBalancer.GetResourceAsync();

            // Assert
            Assert.AreSame(resource1, server1);
            Assert.AreSame(resource2, server2);
            Assert.AreSame(resource3, server3);
            Assert.AreSame(resource4, server1);
        }

        [TestMethod]
        public async Task RoundRobin_Algorithm_ShouldThrowInvalidOperationExceptionIfGetResourceIsCalledFirst()
        { 
            var roundRobinStrategy = new RoundRobinLoadBalancerStrategy<string>();
            var loadBalancer = new LoadBalancer<string>(roundRobinStrategy);

            // Assert
            await Assert.ThrowsExceptionAsync<InvalidOperationException>(loadBalancer.GetResourceAsync); 
        }

        [TestMethod]
        public async Task RoundRobin_Algorithm_ShouldThrowArgumentNullExceptionIfAddedNullResource()
        {
            var roundRobinStrategy = new RoundRobinLoadBalancerStrategy<string>();
            var loadBalancer = new LoadBalancer<string>(roundRobinStrategy);

            // Assert
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() =>
                loadBalancer.AddResourceAsync(null));
        }
    }
}
