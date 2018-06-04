﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace Project.ConsoleApp.Test
{
    [TestClass]
    public class ConsoleAppTest
    {
        string _validJSONFile = string.Empty;
        string _invalidJSONFile = string.Empty;
        string _validXMLFile = string.Empty;
        string _invalidXMLFile = string.Empty;        

        public ConsoleAppTest()
        {
            string testFilePath = Directory.GetCurrentDirectory() + @"\TestFiles\";

            _validJSONFile = testFilePath + "JSON_ValidFormat.json";
            _invalidJSONFile = testFilePath + "JSON_InvalidFormat.json";
            _validXMLFile = testFilePath + "XML_ValidFormat.xml";
            _invalidXMLFile = testFilePath + "XML_InvalidFormat.xml";
        }

        [TestMethod]
        public void AllTestFilesExist()
        {
            Assert.IsTrue(File.Exists(_validJSONFile));
            Assert.IsTrue(File.Exists(_invalidJSONFile));
            Assert.IsTrue(File.Exists(_validXMLFile));
            Assert.IsTrue(File.Exists(_invalidXMLFile));
        }

        [TestMethod]
        public void Arguments_WithValidArguments()
        {
            //assert
            try
            {
                ConsoleApp.Main(new string[] { "-json", _validJSONFile, "-xml", _validXMLFile });
            }
            catch(ArgumentException aex)
            {
                Assert.Fail(aex.Message);
            }            
        }

        [TestMethod]
        public void Arguments_WithInvalidArguments()
        {
            //assert
            try
            {
                ConsoleApp.Main(new string[] { "json", _validJSONFile, "xml", _validXMLFile });
            }
            catch (ArgumentException aex)
            {
                Assert.IsTrue(aex.Message == "Invalid arguments.");
            }
        }

        [TestMethod]
        public void Arguments_WithTooManyArguments()
        {
            //assert
            try
            {
                ConsoleApp.Main(new string[] { "-json", _validJSONFile, "-xml", _validXMLFile, "-test", _validXMLFile });
            }
            catch (ArgumentException aex)
            {
                Assert.Fail(aex.Message);
            }
        }

        [TestMethod]
        public void Arguments_WithMissingArguments()
        {
            //assert
            try
            {
                ConsoleApp.Main(Array.Empty<string>());
            }
            catch (ArgumentException aex)
            {
                Assert.IsTrue(aex.Message == "Invalid arguments.");
            }
        }
    }
}
