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
