using System;
using System.Collections.Generic;
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
    /// Class used for reading GMA files.
    /// </summary>
    public class Reader
    {
        #region PrivateMembers
        private string _filePath;
        private ushort _formatVersion;
        private ulong _steamId;
        private ulong _timestamp;
        private string _name;
        private string _desc;
        private string _author;
        private int _addonVersion;
        private long _fileBlock;

        private List<FileFormat.FileEntry> _files = new List<FileFormat.FileEntry>();
        #endregion

        // Public properties that can only be read
        /// <summary>
        /// GMAD version
        /// </summary>
        public ushort FormatVersion { get { return _formatVersion; } }

        /// <summary>
        /// 64-bit Steam ID of the author
        /// </summary>
        public ulong SteamId { get { return _steamId; } }

        /// <summary>
        /// Unix time stamp of when the file was created
        /// </summary>
        public ulong Timestamp { get { return _timestamp; } }

        /// <summary>
        /// Name of the addon
        /// </summary>
        public string Name { get { return _name; } }

        /// <summary>
        /// Description of the addon
        /// </summary>
        public string Description { get { return _desc; } }

        /// <summary>
        /// The name of the author
        /// </summary>
        public string Author { get { return _author; } }

        /// <summary>
        /// The version of this addon
        /// </summary>
        public int AddonVersion { get { return _addonVersion; } }

        /// <summary>
        /// A list of files found in the GMA file
        /// </summary>
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
        /// Loads a GMAD file with given path and extracts properties and files list
        /// </summary>
        /// <param name="path">Path to .gmad file</param>
        public void Parse(string path)
        {
            _filePath = path;

            using (var r = new BinaryReader(File.Open(path, FileMode.Open)))
            {
                var length = r.BaseStream.Length;
                var ident = Encoding.ASCII.GetString(r.ReadBytes(4));

                if (ident != "GMAD")
                    throw new GmadReaderException("File is not a GMAD file. Got ident " + ident);

                _formatVersion = r.ReadByte();

                if (_formatVersion > FileFormat.Version)
                    throw new GmadReaderException("File is in a newer format than this version of GMAD.NET supports. Format version is " + _formatVersion);

                _steamId = BitConverter.ToUInt64(r.ReadBytes(8), 0);
                _timestamp = BitConverter.ToUInt64(r.ReadBytes(8), 0);

                // Not used
                if (_formatVersion > 1)
                {
                    var content = r.ReadNullTerminatedString();

                    while (!String.IsNullOrEmpty(content))
                    {
                        content = r.ReadNullTerminatedString();
                    }
                }

                _name = r.ReadNullTerminatedString();
                _desc = r.ReadNullTerminatedString();
                _author = r.ReadNullTerminatedString();

                _addonVersion = r.ReadInt32();

                long offset = 0;
                uint fileno = 1;

                // Files block
                while (r.ReadUInt32() != 0)
                {
                    FileFormat.FileEntry entry;

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

                _fileBlock = r.BaseStream.Position;
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
                fs.Position = _fileBlock + entry.Offset;
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
