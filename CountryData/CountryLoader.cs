using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;

namespace CountryData
{
    public static class CountryLoader
    {
        static ConcurrentDictionary<string, List<State>> cache = new ConcurrentDictionary<string, List<State>>();
        static Assembly assembly;
        static JsonSerializer serializer;

        static CountryLoader()
        {
            serializer = new JsonSerializer();
            assembly = typeof(CountryLoader).Assembly;
        }

        public static List<State> Load(string countryCode)
        {
            countryCode = countryCode.ToUpperInvariant();
            return cache.GetOrAdd(countryCode, Inner);
        }

        static List<State> Inner(string countryCode)
        {
            var stream = assembly.GetManifestResourceStream("CountryData.postcodes.zip");
            using (var archive = new ZipArchive(stream))
            {
                var entry = archive.Entries.SingleOrDefault(x => x.Name == $"{countryCode}.json.txt");
                if (entry == null)
                {
                    throw new Exception($"Could not find data for '{countryCode}'.");
                }

                using (var entryStream = entry.Open())
                using (var streamReader = new StreamReader(entryStream))
                using (var jsonTextReader = new JsonTextReader(streamReader))
                {
                    return serializer.Deserialize<List<State>>(jsonTextReader);
                }
            }
        }
    }
}