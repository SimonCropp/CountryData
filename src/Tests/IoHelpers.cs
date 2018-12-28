﻿using System.IO;

static class IoHelpers
{
    public static void PurgeDirectory(string directory)
    {
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
            return;
        }
        var root = new DirectoryInfo(directory);

        foreach (var file in root.GetFiles())
        {
            file.Delete();
        }
        foreach (var dir in root.GetDirectories())
        {
            dir.Delete(true);
        }
    }
}