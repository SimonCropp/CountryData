using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Reflection;

namespace CountryData
{
    public static class CountryLoader
    {
        static ConcurrentDictionary<string, IReadOnlyList<State>> cache = new ConcurrentDictionary<string, IReadOnlyList<State>>();
        static Assembly assembly;

        static CountryLoader()
        {
            assembly = typeof(CountryLoader).Assembly;
            using (var stream = assembly.GetManifestResourceStream("CountryData.countryInfo.json.txt"))
            {
                CountryInfo = Serializer.Deserialize<List<CountryInfo>>(stream).Cast<ICountryInfo>().ToList();
            }
        }

        public static IReadOnlyList<ICountryInfo> CountryInfo { get; }

        public static IReadOnlyDictionary<string, IReadOnlyList<State>> LoadedLocationData => cache;

        public static IReadOnlyList<State> LoadLocationData(string countryCode)
        {
            countryCode = countryCode.ToUpperInvariant();
            return cache.GetOrAdd(countryCode, Inner);
        }

        static List<State> Inner(string countryCode)
        {
            using (var stream = assembly.GetManifestResourceStream("CountryData.postcodes.zip"))
            using (var archive = new ZipArchive(stream))
            {
                var entry = archive.Entries.SingleOrDefault(x => x.Name == $"{countryCode}.json.txt");
                if (entry == null)
                {
                    throw new Exception($"Could not find data for '{countryCode}'.");
                }

                return DeserializeEntry(entry);
            }
        }

        public static void LoadAll()
        {
            using (var stream = assembly.GetManifestResourceStream("CountryData.postcodes.zip"))
            using (var archive = new ZipArchive(stream))
            {
                foreach (var entry in archive.Entries)
                {
                    cache[entry.Name] = DeserializeEntry(entry);
                }
            }
        }

        static List<State> DeserializeEntry(ZipArchiveEntry entry)
        {
            using (var entryStream = entry.Open())
            {
                return Serializer.Deserialize<List<State>>(entryStream);
            }
        }
    }
}