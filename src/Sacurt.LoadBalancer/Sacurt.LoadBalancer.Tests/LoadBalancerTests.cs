using Sacurt.Load_Balancer;
using Sacurt.LoadBalancer.Tests.Common;

namespace Sacurt.LoadBalancer.Tests
{
    [TestClass]
    public partial class LoadBalancerTests
    {
        [TestMethod]
        public void AddResource_ShouldAddResourceAndRetrieveTheSameResource_WithDefaultStrategy()
        {
            // Arrange 
            var loadBalancer = LoadBalancerFactory.DefaultStrategy<Machine>();
            var machine = new Machine("myserver.com");

            // Act 
            loadBalancer.AddResource(machine);

            var firstResource = loadBalancer.GetResource();
            var secondResource = loadBalancer.GetResource(); 

            // Assert
            Assert.AreSame(machine, firstResource);
            Assert.AreSame(machine, secondResource);
        }

        [TestMethod]
        public void GetResource_ShouldFollowRoundRobinStrategy_WithDefaultStrategy()
        {
            // Arrange
            var server1 = "server1";
            var server2 = "server2";
            var server3 = "server3";
                          
            var loadBalancer = LoadBalancerFactory.DefaultStrategy<string>();

            // Act
            loadBalancer.AddResource(server1);
            loadBalancer.AddResource(server2);
            loadBalancer.AddResource(server3);

            var resource1 = loadBalancer.GetResource();
            var resource2 = loadBalancer.GetResource();
            var resource3 = loadBalancer.GetResource();
            var resource4 = loadBalancer.GetResource();

            // Assert
            Assert.AreSame(resource1, server1);
            Assert.AreSame(resource2, server2);
            Assert.AreSame(resource3, server3);
            Assert.AreSame(resource4, server1);
        }

        [TestMethod]
        public void GetResource_ShouldThrowInvalidOperationExceptionIfGetResourceIsCalledFirst()
        {
            // Arrange
            var loadBalancer = LoadBalancerFactory.DefaultStrategy<string>();

            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(loadBalancer.GetResource); 
        }

        [TestMethod]
        public void AddResource_ShouldThrowInvalidOperationExceptionWhenAddDuplicatedResource()
        {
            // Arrange
            var loadBalancer = LoadBalancerFactory.DefaultStrategy<Machine>();
            var machine1 = new Machine("myserver1.com");
            var machine2 = new Machine("myserver2.com");
            var machine3 = new Machine("myserver3.com");
            var repeatedMachine = new Machine("myserver1.com");

            // Act
            loadBalancer.AddResource(machine1);
            loadBalancer.AddResource(machine2);
            loadBalancer.AddResource(machine3);

            // Assert
            Assert.ThrowsException<InvalidOperationException>(() => loadBalancer.AddResource(repeatedMachine));
        }

        [TestMethod]
        public void AddResource_ShouldThrowArgumentNullExceptionIfAddedNullResource()
        {
            // Arrange
            var loadBalancer = LoadBalancerFactory.DefaultStrategy<string>();

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => loadBalancer.AddResource(null));
        }
    }
}
