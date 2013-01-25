/*
 *
 * Copyright (C) 2013 Stephen Swires
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including
 * without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject
 * to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
 * IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE
 * SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 *
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using GMAD.NET.Addon;

namespace GMAD.NET
{
    public static class BufferExtensions
    {
        /// <summary>
        /// Reads C-style NULL terminated string from BinaryReader stream
        /// </summary>
        /// <param name="r"></param>
        /// <returns>UTF-8 encoded .NET string</returns>
        public static string ReadNullTerminatedString(this BinaryReader r)
        {
            var l = new List<byte>();
            byte b;

            while ((b = r.ReadByte()) != 0x00)
                l.Add(b);

            return (l.Count > 0 ? Encoding.UTF8.GetString(l.ToArray()) : "");
        }
    }

    /// <summary>
    /// Exception type used by Reader
    /// </summary>
    public class GmadReaderException : System.Exception
    {
        public GmadReaderException()
        {
            
        }
        
        public GmadReaderException(string message) : base(message)
        {
            
        }
    }

    /// <summary>
    /// Class used for reading GMAD package files.
    /// </summary>
    public class Reader
    {
        #region PrivateMembers
        private string _filePath;
        private FileFormat.Header _header = new FileFormat.Header();
        private List<FileFormat.FileEntry> _files = new List<FileFormat.FileEntry>();
        #endregion

        // Public properties that can only be read
        public FileFormat.Header Header { get { return _header; } }

        /// <summary>
        /// A list of files found in the GMA file
        /// </summary>
        [Browsable(false)]
        public List<FileFormat.FileEntry> Files { get { return _files; } } 

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="path">Optional path, will parse if passed</param>
        public Reader(string path = null)
        {
            if(!String.IsNullOrEmpty(path))
                Parse(path);
        }

        /// <summary>
        /// Reset all fields before parsing
        /// </summary>
        public void Reset()
        {
            _filePath = String.Empty;
            _header.Version = 0;
            _header.SteamId = 0;
            _header.Timestamp = 0;
            _header.Name = String.Empty;
            _header.Description = String.Empty;
            _header.AddonVersion = 0;
            _header.FileBlock = 0;

            _files.Clear();
        }

        /// <summary>
        /// Loads a GMAD file with given path and extracts properties and files list
        /// </summary>
        /// <param name="path">Path to .gmad file</param>
        public void Parse(string path)
        {
            Reset();

            _filePath = path;

            using (var r = new BinaryReader(File.Open(path, FileMode.Open)))
            {
                var length = r.BaseStream.Length;
                _header.Ident = Encoding.ASCII.GetString(r.ReadBytes(4));

                if (_header.Ident != "GMAD")
                    throw new GmadReaderException("File is not a GMAD file. Got ident " + _header.Ident);

                _header.Version = r.ReadByte();

                if (_header.Version > FileFormat.Version)
                    throw new GmadReaderException("File is in a newer format than this version of GMAD.NET supports. Format version is " + _header.Version);

                _header.SteamId = BitConverter.ToUInt64(r.ReadBytes(8), 0);
                _header.Timestamp = BitConverter.ToUInt64(r.ReadBytes(8), 0);

                // Not used
                if (_header.Version > 1)
                {
                    var content = r.ReadNullTerminatedString();

                    while (!String.IsNullOrEmpty(content))
                    {
                        content = r.ReadNullTerminatedString();
                    }
                }

                _header.Name = r.ReadNullTerminatedString();
                _header.Description = r.ReadNullTerminatedString();
                _header.Author = r.ReadNullTerminatedString();

                _header.AddonVersion = r.ReadInt32();

                long offset = 0;
                uint fileno = 1;

                // Files block
                while (r.ReadUInt32() != 0)
                {
                    var entry = new FileFormat.FileEntry();

                    entry.StrName = r.ReadNullTerminatedString();
                    entry.Size = r.ReadInt64();
                    entry.CRC = r.ReadUInt32();
                    entry.Offset = offset;
                    entry.FileNumber = fileno;

                    offset += entry.Size;
                    fileno++;

                    _files.Add(entry);
                }

                if(_files.Count < 1)
                    throw new GmadReaderException("Got no files from GMAD package.");

                _header.FileBlock = r.BaseStream.Position;
            }
        }

        /// <summary>
        /// Number of files
        /// </summary>
        /// <returns>The number of files in the parsed GMAD file</returns>
        public int FileCount()
        {
            return _files.Count;
        }

        /// <summary>
        /// Gets a file inside a GMAD file
        /// </summary>
        /// <param name="entry">FileEntry for the file to be read</param>
        /// <returns>Raw bytes of file in GMAD file</returns>
        public byte[] GetFile(FileFormat.FileEntry entry)
        {
            var buffer = new byte[entry.Size];

            using (var fs = new FileStream(_filePath, FileMode.Open))
            {
                fs.Position = _header.FileBlock + entry.Offset;
                fs.Read(buffer, 0, buffer.Length); // potential problem: what if a file is >2GB (likely?)
            }

            return buffer;
        }

        /// <summary>
        /// GetFile alias that uses a file id
        /// </summary>
        /// <param name="fileid">1-indexed file ID</param>
        /// <returns>Raw bytes of file in GMAD file</returns>
        public byte[] GetFile(int fileid)
        {
            return GetFile(_files[fileid - 1]);
        }
    }
}
