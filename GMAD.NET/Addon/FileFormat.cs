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
