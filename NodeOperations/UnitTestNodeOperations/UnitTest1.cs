using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NodeOperations;

namespace UnitTestNodeOperations
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "null or empty char is not accepted")]
        public void TestDeleteWithNull()//null input is not accepted
        {
            LinkedListHelpers.Node node = new LinkedListHelpers.Node('E');
            node.deleteAllMMoreThanTwoSame(default(Char));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "null or empty char is not accepted")]
        public void TestDeleteWithNEmptyChar()//empty input is not accepted
        {
            LinkedListHelpers.Node node = new LinkedListHelpers.Node('E');
            node.deleteAllMMoreThanTwoSame('\0');
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Make sure only Range of A to Z is entered")]
        public void TestInvalidCharEnteredToConstructor()//only A to Z is acceptable
        {
            LinkedListHelpers.Node node = new LinkedListHelpers.Node('a');
            
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Make sure only Range of A to Z is entered")]
        public void TestInvalidCharEnteredToConstructor1()//only A to Z is acceptable
        {
            LinkedListHelpers.Node node = new LinkedListHelpers.Node('0');

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Make sure only Range of A to Z is entered")]
        public void TestInvalidCharEnteredToDeleteAllMMoreThanTwoSame()//only A to Z is acceptable
        {
            LinkedListHelpers.Node node = new LinkedListHelpers.Node('E');
            node.deleteAllMMoreThanTwoSame('b');
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Make sure only Range of A to Z is entered")]
        public void TestInvalidCharEnteredToDeleteAllMMoreThanTwoSame1()//only A to Z is acceptable
        {
            LinkedListHelpers.Node node = new LinkedListHelpers.Node('A');
            node.deleteAllMMoreThanTwoSame('0');
        }

        [TestMethod]
        public void TestValidCharEnteredToConstructor()//only A to Z is acceptable
        {
            try
            {
                LinkedListHelpers.Node node = new LinkedListHelpers.Node('K');
            }catch(Exception ex)
            {
                Assert.Fail("No exception is expected since it is a valid char. "+ex);
            }

        }

        [TestMethod]
        public void TestValidCharEnteredToDeleteAllMMoreThanTwoSame()//only A to Z is acceptable
        {
            try
            {
                LinkedListHelpers.Node node = new LinkedListHelpers.Node('Y');
                node.deleteAllMMoreThanTwoSame('Y');
            }
            catch (Exception ex)
            {
                Assert.Fail("No exception is expected since it is a valid char. " + ex);
            }
        }

        [TestMethod]
        public void TestAddingValidData()//checking if elements are added to the list successfully
        {
            ConsoleOutput consoleOutput = new ConsoleOutput();
            LinkedListHelpers.Node node= new LinkedListHelpers.Node('E');
            node.addNode('B');//data from the question
            node.addNode('E');
            node.addNode('E');
            node.addNode('B');
            node.addNode('A');
            node.addNode('B');
            node.print();
            //target.WriteToConsole(text);
            Assert.AreEqual("|E||->|B||->|E||->|E||->|B||->|A||->|B||->", consoleOutput.GetOuput().Trim());
            

        }


        [TestMethod]
        public void TestAddingValidDeletingMoreThanThreeData()//checking if elements are deleted from the list successfully
        {
            ConsoleOutput consoleOutput = new ConsoleOutput();
            LinkedListHelpers.Node node = new LinkedListHelpers.Node('E');
            node.addNode('B');//data from the question
            node.addNode('E');
            node.addNode('E');
            node.addNode('B');
            node.addNode('A');
            node.addNode('B');
            node.deleteAllMMoreThanTwoSame('E');
            node.print();
            //target.WriteToConsole(text);
            Assert.AreEqual("|E||->|B||->|E||->|B||->|A||->", consoleOutput.GetOuput().Trim());


        }

        [TestMethod]
        public void TestAddingValidData1()//checking if elements are added to the list successfully
        {
            ConsoleOutput consoleOutput = new ConsoleOutput();
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
            //target.WriteToConsole(text);
            Assert.AreEqual("|A||->|A||->|E||->|E||->|B||->|A||->|B||->|E||->|B||->|F||->", consoleOutput.GetOuput().Trim());


        }


        [TestMethod]
        public void TestAddingValidDeletingMoreThanThreeData1()//checking if elements are deleted from the list successfully
        {
            ConsoleOutput consoleOutput = new ConsoleOutput();
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
            node.deleteAllMMoreThanTwoSame('A');
            node.print();
            //target.WriteToConsole(text);
            Assert.AreEqual("|A||->|A||->|E||->|E||->|B||->|B||->|F||->", consoleOutput.GetOuput().Trim());


        }

        public class ConsoleOutput : IDisposable
        {
            private StringWriter stringWriter;
            private TextWriter originalOutput;

            public ConsoleOutput()
            {
                stringWriter = new StringWriter();
                originalOutput = Console.Out;
                Console.SetOut(stringWriter);
            }

            public string GetOuput()
            {
                return stringWriter.ToString();
            }

            public void Dispose()
            {
                Console.SetOut(originalOutput);
                stringWriter.Dispose();
            }
        }



    }

   

}
