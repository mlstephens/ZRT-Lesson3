﻿using System;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_Xml;

namespace Project_UnitTest
{
    [TestClass]
    public class UnitTest
    {
        string _testFilePath = string.Empty;
        const string _validJSONFile = "JSON_ValidFormat.json";
        const string _invalidJSONFile = "JSON_InvalidFormat.json";
        const string _validXMLFile = "XML_ValidFormat.xml";
        const string _invalidXMLFile = "XML_InvalidFormat.xml";

        public UnitTest()
        {
            _testFilePath = Directory.GetCurrentDirectory() + @"\TestFiles\";
        }

        [TestMethod]
        public void AllTestFilesExist()
        {
            Assert.IsTrue(File.Exists(_testFilePath + _validJSONFile));
            Assert.IsTrue(File.Exists(_testFilePath + _invalidJSONFile));
            Assert.IsTrue(File.Exists(_testFilePath + _validXMLFile));
            Assert.IsTrue(File.Exists(_testFilePath + _invalidXMLFile));
        }

        [TestMethod]
        public void ValidArguments()
        {
            //arrange
            string[] argArray = new string[] { "-json", _testFilePath, "-xml", _testFilePath };

            //act
            bool valid = argArray.Any(a => String.Compare(a, "-json", true) == 0) || argArray.Any(a => String.Compare(a, "-xml", true) == 0);

            //assert
            Assert.IsTrue(valid);
        }

        [TestMethod]
        public void InvalidArguments()
        {
            //arrange
            string[] argArray = new string[] { "json", _testFilePath, "xml", _testFilePath };

            //act
            bool valid = argArray.Any(a => String.Compare(a, "-json", true) == 0) || argArray.Any(a => String.Compare(a, "-xml", true) == 0);

            //assert
            Assert.IsFalse(valid);
        }

        #region  ' Xml Tests '

        [TestMethod]
        public void XmlArgument_WithValidAbsolutePath()
        {
            //assert
            Assert.IsTrue(File.Exists(new ArgumentXml().GetFilePathFromArgument(new string[] { "-xml", _testFilePath + _validXMLFile }, "-xml")));
        }

        [TestMethod]
        public void XmlArgument_WithValidRelativePath()
        {
            //assert
            Assert.IsTrue(Directory.Exists(new ArgumentXml().GetFilePathFromArgument(new string[] { "-xml", _testFilePath }, "-xml")));
        }

        [TestMethod]
        public void XmlArgument_WithTooManyArguments()
        {
            //assert
            Assert.IsTrue(File.Exists(new ArgumentXml().GetFilePathFromArgument(new string[] { "-xml", _testFilePath + _validXMLFile, "-test1", _testFilePath, "-test2", _testFilePath }, "-xml")));
        }

        [TestMethod]
        public void XmlArgument_WithInvalidArgument()
        {
            //assert
            Assert.IsTrue(string.IsNullOrEmpty(new ArgumentXml().GetFilePathFromArgument(new string[] { "xml", _testFilePath }, "-xml")));
        }

        [TestMethod]
        public void XmlArgument_WithMissingArgument()
        {
            //assert
            Assert.IsTrue(string.IsNullOrEmpty(new ArgumentXml().GetFilePathFromArgument(Array.Empty<string>(), "-xml")));
        }

        [TestMethod]
        public void XmlFile_WithValidFormat()
        {
            //act
            var xmlItems = new ArgumentXml().GetValuesFromFile(_testFilePath + _validXMLFile);

            //assert
            Assert.IsTrue(xmlItems.Length > 0);
            CollectionAssert.AllItemsAreNotNull(xmlItems, "Null values in XML.");
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "No Exception was thrown.")]
        public void XmlFile_WithInvalidFormat()
        {
            //act
            var xmlItems = new ArgumentXml().GetValuesFromFile(_testFilePath + _invalidJSONFile);
        }

        #endregion
    }
}
