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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMAD.NET.Addon;

namespace GMAD.NET
{
    public static class WriterUtils
    {
        public static void WriteNullTerminatedString(this BinaryWriter bw, string str, Encoding enc = null)
        {
            if (enc == null)
                enc = Encoding.UTF8;

            var buffer = enc.GetBytes(str);
            bw.Write(buffer);
            bw.Write((byte)0);
        }
    }

    /// <summary>
    /// Exception type used by Writer
    /// </summary>
    public class GmadWriterException : System.Exception
    {
        public GmadWriterException()
        {

        }

        public GmadWriterException(string message)
            : base(message)
        {

        }
    }

    public class Writer
    {
        public FileFormat.Header Header { get; set; }
        public List<FileFormat.FileEntry> Files { get; set; }

        // used when modifying
        public Reader Reader { get; set; }

        public Writer()
        {
            Reader = null;
        }

        public void Write(string path)
        {
            using (var bw = new BinaryWriter(new FileStream(path, FileMode.Create, FileAccess.Write)))
            {
                bw.Write(Encoding.ASCII.GetBytes("GMAD"));

                bw.Write((byte)Header.Version);
                bw.Write(Header.SteamId);
                bw.Write(Convert.ToUInt64((Header.Timestamp - (new DateTime(1970, 1, 1, 0, 0, 0))).TotalSeconds));
                
                bw.Write((byte)0); // will be content block

                bw.WriteNullTerminatedString(Header.Name);
                bw.WriteNullTerminatedString(Header.Description);
                bw.WriteNullTerminatedString(Header.Author);
                bw.Write(Header.AddonVersion);

                // File table block
                uint fileNumber = 1;

                foreach (var file in Files)
                {
                    bw.Write(fileNumber);
                    bw.WriteNullTerminatedString(file.StrName);
                    bw.Write(file.Size);
                    bw.Write(file.CRC);

                    fileNumber++;
                }

                // End file table
                bw.Write(0);

                // File data
                foreach (var file in Files)
                {
                    if (file.LocalFile == false)
                    {
                        if(Reader != null)
                            bw.Write(Reader.GetFile(file));
                        else
                            throw new GmadWriterException("Using files from existing gmad file, but no reader has been given.");
                    }
                    else
                    {
                        // read file from disk
                        using (var fs = new FileStream(file.PhysicalPath, FileMode.Open, FileAccess.Read))
                        {
                            var buffer = new byte[fs.Length];
                            fs.Read(buffer, 0, buffer.Length);

                            bw.Write(buffer);
                        }
                    }
                }

            }
        }
    }
}
