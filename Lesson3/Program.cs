﻿using Project_Interface;
using Project_Xml;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Project_Console
{
    class Program
    {
        [STAThread]
        static void Main(string[] clArguments)
        {
            if (clArguments.Any(a => String.Compare(a, "-xml", true) == 0))
            {
                IFileHandling ifh =  new ArgumentXml(clArguments);

                //create a list then order it so nulls are last in the list
                List<string> parsedData = ifh.GetParsedData("-xml")
                    .OrderBy(fh => fh)
                    .ToList()
                    .OrderBy(ai => ai == null)
                    .ToList();

                //display the list and replace nulls with No Value
                ifh.DisplayData(parsedData);
            }
            else
            {
                Console.WriteLine("Invalid arguments.");
            }
        }
    }
}
