using Sacurt.Load_Balancer;
using Sacurt.LoadBalancer.Tests.Common;

namespace Sacurt.LoadBalancer.Tests
{ 
    public partial class LoadBalancerTests
    {
        [TestMethod]
        public void GetResource_ShouldReturnRandomMachine()
        {
            // Arrange
            var random = new Random(5); // Used with seed for deterministic output
            LoadBalancer<Machine> loadBalancer = LoadBalancerFactory.RandomStrategy<Machine>(random);

            var server1 = new Machine("server1");
            var server2 = new Machine("server2");
            var server3 = new Machine("server3");

            loadBalancer.AddResource(server1);
            loadBalancer.AddResource(server2);
            loadBalancer.AddResource(server3);

            var resource1 = loadBalancer.GetResource();
            var resource2 = loadBalancer.GetResource();
            var resource3 = loadBalancer.GetResource();

            Assert.AreEqual(server2, resource1);
            Assert.AreEqual(server3, resource2);
            Assert.AreEqual(server3, resource3);
        }

        [TestMethod]
        public void GetResource_ShouldAddResourceAndRetrieveTheSameResource()
        {
            // Arrange 
            LoadBalancer<Machine> loadBalancer = LoadBalancerFactory.RandomStrategy<Machine>();
            var machine = new Machine("machine.com:7080");

            // Act
            loadBalancer.AddResource(machine);

            //Assert
            Enumerable.Range(0, 20)
                .ToList()
                .ForEach(i =>
                    Assert.AreEqual(machine, loadBalancer.GetResource()));
        }

        [TestMethod]
        public void GetResource_ShouldBeThreadSafeInRandomScenario()
        {
            // Arrange 
            LoadBalancer<Machine> loadBalancer = LoadBalancerFactory.RandomStrategy<Machine>();

            Parallel.For(1, 100, i => loadBalancer.AddResource(new Machine($"machine_{i}.com")));

            // Act
            var retrieved = Parallel.For(0, 100, i => loadBalancer.GetResource());

            // Assert
            Assert.IsTrue(retrieved.IsCompleted);
        }
    }
}
