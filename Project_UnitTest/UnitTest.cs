﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Project_Console;
using System;
using System.IO;
using System.Xml;

namespace Project_UnitTest
{
    [TestClass]
    public class UnitTest
    {
        const string _validJSONFile = "JSON_ValidFormat.json";
        const string _invalidJSONFile = "JSON_InvalidFormat.json";
        const string _validXMLFile = "XML_ValidFormat.xml";
        const string _invalidXMLFile = "XML_InvalidFormat.xml";

        string _testFilePath = string.Empty;

        IFileHandling _ifhJson = new ArgumentJson();
        IFileHandling _ifhXml = new ArgumentXml();

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
        public void Arguments_WithValidArguments()
        {
            //arrange
            string[] argArray = new string[] { "-json", _testFilePath + _validJSONFile, "-xml", _testFilePath + _validXMLFile };

            //assert
            Assert.IsTrue(File.Exists(_ifhJson.GetFilePath(argArray, "-json")) && File.Exists(_ifhXml.GetFilePath(argArray, "-xml")));
        }

        [TestMethod]
        public void Arguments_WithInvalidArguments()
        {
            //arrange
            string[] argArray = new string[] { "json", _testFilePath + _validJSONFile, "xml", _testFilePath + _validXMLFile };

            //act
            bool invalidArguments = string.IsNullOrEmpty(_ifhJson.GetFilePath(argArray, "-json")) &&
                                    string.IsNullOrEmpty(_ifhXml.GetFilePath(argArray, "-xml"));

            //assert
            Assert.IsTrue(invalidArguments);
        }

        [TestMethod]
        public void Arguments_WithTooManyArguments()
        {
            //arrange
            string[] argArray = new string[] { "-json", _testFilePath + _validJSONFile, "-xml", _testFilePath + _validXMLFile, "-test", _testFilePath };

            //assert
            Assert.IsTrue(File.Exists(_ifhJson.GetFilePath(argArray, "-json")));
            Assert.IsTrue(File.Exists(_ifhXml.GetFilePath(argArray, "-xml")));
        }

        [TestMethod]
        public void Arguments_WithInvalidJSONArgument() => Assert.IsTrue(string.IsNullOrEmpty(_ifhJson.GetFilePath(new string[] { "json", _testFilePath + _validJSONFile }, "-json")));

        [TestMethod]
        public void Arguments_WithInvalidXMLArgument() => Assert.IsTrue(string.IsNullOrEmpty(_ifhXml.GetFilePath(new string[] { "xml", _testFilePath + _validXMLFile }, "-xml")));

        [TestMethod]
        public void Arguments_WithMissingArguments()
        {
            //act
            bool missingArguments = string.IsNullOrEmpty(_ifhJson.GetFilePath(Array.Empty<string>(), "-json")) &&
                                    string.IsNullOrEmpty(_ifhXml.GetFilePath(Array.Empty<string>(), "-xml"));

            //assert
            Assert.IsTrue(missingArguments);
        }

        [TestMethod]
        public void File_WithValidXMLFormat() => Assert.IsTrue(_ifhXml.ParseFileData(_testFilePath + _validXMLFile).Length > 0);

        [TestMethod]
        public void File_WithInvalidXMLFormat() => Assert.ThrowsException<XmlException>(() => _ifhXml.ParseFileData(_testFilePath + _invalidXMLFile));

        [TestMethod]
        public void File_WithValidJSONFormat() => Assert.IsTrue(_ifhJson.ParseFileData(_testFilePath + _validJSONFile).Length > 0);

        [TestMethod]
        public void File_WithInvalidJSONFormat() => Assert.ThrowsException<JsonSerializationException>(() => _ifhJson.ParseFileData(_testFilePath + _invalidJSONFile));
    }
}
