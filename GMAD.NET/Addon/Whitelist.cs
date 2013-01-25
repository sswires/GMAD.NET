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
using System.Text.RegularExpressions;

namespace GMAD.NET.Addon
{
    /// <summary>
    /// Whitelist helpers
    /// </summary>
    public static class Whitelist
    {
        /// <summary>
        /// String array of regex patterns allowed in GMAD files
        /// </summary>
        public static readonly string[] Wildcard =
            {
                @"maps/(.*)\.bsp",
                @"maps/(.*)\.png",
                @"maps/(.*)\.nav",
                @"maps/(.*)\.ain",
                @"sound/(.*)\.wav",
                @"sound/(.*)\.mp3",
                @"lua/(.*)\.lua",
                @"materials/(.*)\.vmt",
                @"materials/(.*)\.vtf",
                @"materials/(.*)\.png",
                @"models/(.*)\.mdl",
                @"models/(.*)\.vtx",
                @"models/(.*)\.phy",
                @"models/(.*)\.ani",
                @"models/(.*)\.vvd",
                @"gamemodes/(.*)\.txt",
                @"gamemodes/(.*)\.lua",
                @"scenes/(.*)\.vcd",
                @"particles/(.*)\.pcf",
                @"gamemodes/(.*)/backgrounds/(.*)\.jpg",
                @"gamemodes/(.*)/icon24\.png",
                @"gamemodes/(.*)/logo\.png",
                @"scripts/vehicles/(.*)\.txt",
                @"resource/fonts/(.*)\.ttf",
            };

        /// <summary>
        /// Check a file is allowed to be added to a GMA file
        /// </summary>
        /// <param name="filename">Name of file we're trying to add</param>
        /// <returns>True if file is valid</returns>
        public static bool Check(string filename)
        {
            foreach (var rule in Wildcard)
            {
                var match = Regex.Match(filename, rule, RegexOptions.IgnoreCase);

                if (match.Success)
                    return true;
            }

            return false;
        }

    }
}
