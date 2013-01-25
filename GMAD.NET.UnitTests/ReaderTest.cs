using System;
using System.Diagnostics;
using System.Text;
using GMAD.NET;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GMAD.NET.UnitTests
{
    [TestClass]
    public class ReaderTest
    {
        [TestMethod]
        public void ReadGmadData()
        {
            var gmadReader = new Reader(@"E:\Program Files\steam\steamapps\sourcemods\garrysmod\addons\lua_rollercoasters_104508032.gma");

            Debug.WriteLine("Title: " + gmadReader.Header.Name);
            Debug.WriteLine("Author: " + gmadReader.Header.Author);
            Debug.WriteLine(gmadReader.Header.Description);
            Debug.WriteLine("File count: " + gmadReader.Files.Count);
            Debug.WriteLine("");

            foreach (var file in gmadReader.Files)
            {
                Debug.WriteLine("File: " + file.StrName);
            }
        }

        [TestMethod]
        public void ReadFirstFile()
        {
            var gmadReader = new Reader(@"E:\Program Files\steam\steamapps\sourcemods\garrysmod\addons\lua_rollercoasters_104508032.gma");

            if (gmadReader.Files.Count > 0)
            {
                Debug.WriteLine(Encoding.UTF8.GetString(gmadReader.GetFile(1)));
            }
            else
            {
                throw new Exception("No files found in GMAD");
            }
        }
    }
}
