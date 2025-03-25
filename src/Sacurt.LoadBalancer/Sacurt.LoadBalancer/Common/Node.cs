using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sacurt.Load_Balancer.Common
{
    internal record Node<T> 
    {
        public Node(T value) => Value = value;

        public T Value { get; set; }
        public Node<T> Next { get; set; } 
    }
}
