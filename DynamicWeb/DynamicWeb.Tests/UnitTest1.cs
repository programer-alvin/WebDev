using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DynamicWeb.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Invalid file")]
        public void TestInValidFile()//test when file is invalid
        {
            ContentBuilder contentBuilder = new ContentBuilder();
            contentBuilder.readFile("");
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "File should not be empty")]
        public void TestEmptyFile()//empty file supplied
        {
            ContentBuilder contentBuilder = new ContentBuilder();
            string workingDirectory = Environment.CurrentDirectory;//.bin\debug
            string mappath = Directory.GetParent(workingDirectory).Parent.FullName;//class path
            mappath = mappath.Split('.')[0] + "\\";//project dir
            contentBuilder.analyzeText(contentBuilder.readFile(mappath + "empty.data"));//intergration test
            contentBuilder.readFile(mappath + "empty.data");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Data not well fomated. Kindly make sure braces balance")]
        public void TestImbalanceBraces()//file with implanced braces supplied
        {
            ContentBuilder contentBuilder = new ContentBuilder();
            string workingDirectory = Environment.CurrentDirectory;//.bin\debug
            string mappath = Directory.GetParent(workingDirectory).Parent.FullName;//class path
            mappath = mappath.Split('.')[0] + "\\";//project dir
            contentBuilder.analyzeText(contentBuilder.readFile(mappath + "impalancedbraces.data"));//intergration test
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Data not well fomated. Kindly make sure brackets balance")]
        public void TestImbalanceBrackets()//file with implanced brackets supplied
        {
            ContentBuilder contentBuilder = new ContentBuilder();
            string workingDirectory = Environment.CurrentDirectory;//.bin\debug
            string mappath = Directory.GetParent(workingDirectory).Parent.FullName;//class path
            mappath = mappath.Split('.')[0] + "\\";//project dir
            contentBuilder.analyzeText(contentBuilder.readFile(mappath + "implancedbrackets.data"));//intergration test
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "No valid Main Block was found")]
        public void TestFileWithNoMainBlock()//file with no main blocks supplied
        {
            ContentBuilder contentBuilder = new ContentBuilder();
            string workingDirectory = Environment.CurrentDirectory;//.bin\debug
            string mappath = Directory.GetParent(workingDirectory).Parent.FullName;//class path
            mappath = mappath.Split('.')[0] + "\\";//project dir
            contentBuilder.analyzeText(contentBuilder.readFile(mappath + "nomainblocks.data"));//intergration test
        }

        [TestMethod]
        public void TestValidFile()//test when file is okay
        {
            ContentBuilder contentBuilder = new ContentBuilder();
            try {
                string workingDirectory = Environment.CurrentDirectory;//.bin\debug
                string mappath = Directory.GetParent(workingDirectory).Parent.FullName;//class path
                mappath = mappath.Split('.')[0]+ "\\";//project dir
                contentBuilder.readFile(mappath+"menu.data");
            }
            catch (Exception ex)
            {
                Assert.Fail("No exception is expected since data is valid. "+ex);
            }
        }

        [TestMethod]
        public void TestAnalyzeValidFile()//analysis should go through since the data file provided is valid
        {
            ContentBuilder contentBuilder = new ContentBuilder();
            try
            {
                string workingDirectory = Environment.CurrentDirectory;//.bin\debug
                string mappath = Directory.GetParent(workingDirectory).Parent.FullName;//class path
                mappath = mappath.Split('.')[0] + "\\";//project dir
                contentBuilder.readFile(mappath+"menu.data");
                if(contentBuilder.analyzeText(contentBuilder.readFile(mappath + "menu.data")) =="")
                {//intergration test
                    Assert.Fail("The method should return string since valid data was provided");
                }
            }
            catch (Exception ex)
            {
                Assert.Fail("No exception is expected since data is davalid. " + ex.ToString());
            }
        }
    }
}
