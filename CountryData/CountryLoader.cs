using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Reflection;

namespace CountryData
{
    public static partial class CountryLoader
    {
        static ConcurrentDictionary<string, IReadOnlyList<IState>> cache = new ConcurrentDictionary<string, IReadOnlyList<IState>>();
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

        public static IReadOnlyDictionary<string, IReadOnlyList<IState>> LoadedLocationData => cache;

        public static IReadOnlyList<IState> LoadLocationData(string countryCode)
        {
            countryCode = countryCode.ToUpperInvariant();
            return cache.GetOrAdd(countryCode, Inner);
        }

        static IReadOnlyList<IState> Inner(string countryCode)
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