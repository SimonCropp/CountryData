using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

static class RowReader
{
    public static IEnumerable<string?[]> ReadRows(string path)
    {
        var tab = Convert.ToChar(9);
        using var csv = File.OpenText(path);
        while (true)
        {
            var line = csv.ReadLine();
            if (line is null)
            {
                break;
            }

            if (line.StartsWith("#"))
            {
                continue;
            }

            yield return SplitLine(line, tab).ToArray();
        }
    }

    static IEnumerable<string?> SplitLine(string line, char tab)
    {
        var split = line.Split(tab);
        foreach (var s in split)
        {
            if (s.Length == 0)
            {
                yield return null;
            }
            else
            {
                yield return s.Trim().Trim('_').Replace('_', ' ');
            }
        }
    }
}