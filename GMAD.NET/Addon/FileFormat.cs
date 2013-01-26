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
using System.ComponentModel;

namespace GMAD.NET.Addon
{
    /// <summary>
    /// 
    /// </summary>
    public static class FileFormat
    {
        /// <summary>
        /// Initial padding in all GMAD files
        /// </summary>
        public static readonly string Ident = "GMAD";

        /// <summary>
        /// File format version
        /// </summary>
        public static readonly ushort Version = 3;

        /// <summary>
        /// Steam app ID for the addon
        /// </summary>
        public static readonly uint AppID = 4000;

        /// <summary>
        /// 
        /// </summary>
        public static readonly uint CompressionSignature = 0xBEEFCACE;

        /// <summary>
        /// GMAD File header
        /// </summary>
        public class Header
        {
            public Header()
            {
                Ident = FileFormat.Ident;
                Version = FileFormat.Version;
            }

            /// <summary>
            /// Ident in file, this will usually be "GMAD"
            /// </summary>
            [DisplayName("File Ident"), Description("Padding at begining of file, used to identify type."), Category("Version")]
            public string Ident { get; set; }

            /// <summary>
            /// File format version
            /// </summary>
            [DisplayName("Format Version"), Description("Version of the GMAD file format."), Category("Version")]
            public ushort Version { get; set; }

            /// <summary>
            /// 64-bit Steam ID of the author
            /// </summary>
            [DisplayName("Steam ID"), Category("Author"), Description("64-bit Steam ID of the author.")]
            public ulong SteamId { get; set; }

            /// <summary>
            /// Unix time stamp of when the file was created
            /// </summary>
            [Category("Version"), Description("Unix time stamp of when the file was created.")]
            public ulong Timestamp { get; set; }

            /// <summary>
            /// Name of the addon
            /// </summary>
            [Category("Addon"), Description("Name of the addon")]
            public string Name { get; set; }

            /// <summary>
            /// Description of the addon
            /// </summary>
            [Category("Addon"), Description("Description of the addon.")]
            public string Description { get; set; }

            /// <summary>
            /// The name of the author
            /// </summary>
            [Category("Author"), Description("Name of the author.")]
            public string Author { get; set; }

            /// <summary>
            /// The version of this addon
            /// </summary>
            [DisplayName("Addon Version"), Description("Iteration of this addon."), Category("Version")]
            public int AddonVersion { get; set; }

            /// <summary>
            /// The offset at which the file block starts
            /// </summary>
            [Browsable(false)]
            public long FileBlock { get; set; }
        }

        /// <summary>
        /// Represents a single file definition in a GMAD file
        /// </summary>
        public class FileEntry
        {
            public FileEntry()
            {
                LocalFile = false;
                PhysicalPath = String.Empty;
            }

            /// <summary>
            /// Name of file
            /// </summary>
            public string StrName { get; set; }

            /// <summary>
            /// Number of bytes in file
            /// </summary>
            public long Size { get; set; }

            /// <summary>
            /// CRC checksum of file
            /// </summary>
            public uint CRC { get; set; }

            /// <summary>
            /// File number (starts at 1)
            /// </summary>
            public uint FileNumber { get; set; }

            /// <summary>
            /// Offset in bytes of where the file begins in the file block
            /// </summary>
            public long Offset { get; set; }

            /// <summary>
            /// Is this a local file? (used for writer)
            /// </summary>
            public bool LocalFile { get; set; }

            /// <summary>
            /// Physical path to local file. (used for writer)
            /// </summary>
            public string PhysicalPath { get; set; }
        }
    }
}
