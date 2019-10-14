using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeOperations
{
    class Program
    {
        static void Main(string[] args)
        {
            LinkedListHelpers.Node node = new LinkedListHelpers.Node('A');
            node.addNode('A');
            node.addNode('E');
            node.addNode('E');
            node.addNode('B');
            node.addNode('A');
            node.addNode('B');
            node.addNode('E');
            node.addNode('B');
            node.addNode('F');
            node.print();
            //node.deleteTwoSameConcecutive('E');
            node.deleteAllMMoreThanTwoSame('A');
            node.print();
        }
    }
}
