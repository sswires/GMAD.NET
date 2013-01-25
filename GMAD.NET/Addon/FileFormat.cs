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
        public struct Header
        {
            /// <summary>
            /// Ident in file, this will usually be "GMAD"
            /// </summary>
            public string Ident;

            /// <summary>
            /// File format version
            /// </summary>
            public ushort Version;
        }

        /// <summary>
        /// Represents a single file definition in a GMAD file
        /// </summary>
        public struct FileEntry
        {
            /// <summary>
            /// Name of file
            /// </summary>
            public string StrName;

            /// <summary>
            /// Number of bytes in file
            /// </summary>
            public long Size;

            /// <summary>
            /// CRC checksum of file
            /// </summary>
            public ulong CRC;

            /// <summary>
            /// File number (starts at 1)
            /// </summary>
            public uint FileNumber;

            /// <summary>
            /// Offset in bytes of where the file begins in the file block
            /// </summary>
            public long Offset;
        }
    }
}
