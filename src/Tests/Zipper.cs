using System;
using System.IO;
using System.IO.Compression;

static class Zipper
{
    public static void ZipDir(string targetFile, string sourceDirectory)
    {
        File.Delete(targetFile);
        using (var zipStream = File.Create(targetFile))
        using (var archive = new ZipArchive(zipStream, ZipArchiveMode.Create))
        {
            foreach (var file in Directory.EnumerateFiles(sourceDirectory))
            {
                var entry = archive.CreateEntry(Path.GetFileName(file));
                //To stop the zip changing from the perspective of git
                entry.LastWriteTime = new DateTimeOffset(2000, 1, 1, 1, 1, 1, TimeSpan.Zero);

                using (var fileStream = File.OpenRead(file))
                using (var entryStream = entry.Open())
                {
                    fileStream.CopyTo(entryStream);
                }
            }
        }
    }
}