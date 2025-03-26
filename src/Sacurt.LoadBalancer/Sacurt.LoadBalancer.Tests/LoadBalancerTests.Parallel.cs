using System.Collections.Concurrent;
using Sacurt.Load_Balancer;
using Sacurt.LoadBalancer.Tests.Common;

namespace Sacurt.LoadBalancer.Tests
{ 
    public partial class LoadBalancerTests
    {
        [TestMethod]
        public void GetResource_ShouldEnsureFunctionalityUnderConcurrency_WithDefaultStrategy()
        {
            // Arrange 
            var loadBalancer = LoadBalancerFactory.DefaultStrategy<string>();
            var server1 = "server1";
            var server2 = "server2";
            var server3 = "server3";
            var resultDictionary = new ConcurrentDictionary<string, int>();
            var counter = 60;

            // Act              
            loadBalancer.AddResource(server1);
            loadBalancer.AddResource(server2);
            loadBalancer.AddResource(server3);

            Parallel.For(0, counter, (i) =>
            {
                var resource = loadBalancer.GetResource();
                resultDictionary.AddOrUpdate(resource, 1, (key, value) => value + 1);
            });             

            // Assert
            Assert.AreEqual(counter / 3, resultDictionary[server1]);
            Assert.AreEqual(counter / 3, resultDictionary[server2]);
            Assert.AreEqual(counter / 3, resultDictionary[server3]);
        }

        [TestMethod]
        public async Task GetResource_ShouldEnsureConcurrentExecutionWithTasks()
        {
            // Arrange
            int taskCount = 100;
            var loadBalancer = LoadBalancerFactory.RoundRobinStrategy<Machine>();

            var resources = new ConcurrentBag<Machine>()
            {
                new Machine( "https://mydomain.com/api/orders"),
                new Machine( "https://mydomain.com/api/products"),
                new Machine( "https://mydomain.com/api/shipping"),
                new Machine( "https://mydomain.com/api/stocks"),
            };

            // Act 
            Parallel.ForEach(resources, loadBalancer.AddResource);

            var tasks = new Task[taskCount];
            var results = new ConcurrentDictionary<string, int>();

            for (int i = 0; i < taskCount; i++)
            {
                tasks[i] = Task.Run(() =>
                {
                    var resource = loadBalancer.GetResource();
                    results.AddOrUpdate(resource.Url, 1, (_, count) => count + 1);
                });
            }

            await Task.WhenAll(tasks);

            // Assert
            int expectedResourceDistribuition = taskCount / resources.Count;

            foreach (var resource in resources)
            {
                Assert.AreEqual(expectedResourceDistribuition, results[resource.Url]);
            }
        }
    }
}
