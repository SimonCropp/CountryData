using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

static class RowReader
{
    public static IEnumerable<string[]> ReadRows(string path)
    {
        var tab = Convert.ToChar(9);
        using (var csv = File.OpenText(path))
        {
            while (true)
            {
                var line = csv.ReadLine();
                if (line == null)
                {
                    break;
                }

                if (line.StartsWith("#"))
                {
                    continue;
                }
                var split = line.Split(tab);
                for (var index = 0; index < split.Length; index++)
                {
                    var s = split[index];
                    if (s.Length == 0)
                    {
                        split[index] = null;
                        continue;
                    }

                    split[index] = s.Trim().Trim('_').Replace('_',' ');
                }

                yield return split;
            }
        }
    }
}