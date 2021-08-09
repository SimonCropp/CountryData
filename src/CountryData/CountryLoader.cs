﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;

namespace CountryData
{
    public static partial class CountryLoader
    {
        static ConcurrentDictionary<string, ICountry> cache = new();
        static Assembly assembly;

        static CountryLoader()
        {
            assembly = typeof(CountryLoader).Assembly;
            using var stream = assembly.GetManifestResourceStream("CountryData.countryInfo.json.txt");

            CountryInfo = Serializer.Deserialize<List<CountryInfo>>(stream);
        }

        public static IReadOnlyList<ICountryInfo> CountryInfo { get; }

        public static IReadOnlyDictionary<string, ICountry> LoadedLocationData => cache;

        public static ICountry LoadLocationData(string countryCode)
        {
            Guard.AgainstNullWhiteSpace(countryCode, nameof(countryCode));
            countryCode = countryCode.ToUpperInvariant();
            return cache.GetOrAdd(countryCode, Inner);
        }

        static Country Inner(string countryCode)
        {
            using var stream = assembly.GetManifestResourceStream("CountryData.postcodes.zip")!;
            using ZipArchive archive = new(stream);
            var entry = archive.Entries.SingleOrDefault(x => x.Name == $"{countryCode}.json.txt");
            if (entry is null)
            {
                throw new ArgumentException($"Could not find data for '{countryCode}'.");
            }

            return ConstructCountry(entry, countryCode);
        }

        static Country ConstructCountry(ZipArchiveEntry entry, string countryCode)
        {
            var countryInfo = CountryInfo.Single(x => x.Iso == countryCode);
            Country country = new()
            {
                Name = countryInfo.Name,
                Iso = countryInfo.Iso,
                PostCodeFormat = countryInfo.PostCodeFormat,
                Population = countryInfo.Population,
                Continent = countryInfo.Continent,
                Iso3 = countryInfo.Iso3,
                Fips = countryInfo.Fips,
                CurrencyCode = countryInfo.CurrencyCode,
                Area = countryInfo.Area,
                Capital = countryInfo.Capital,
                IsoNumeric = countryInfo.IsoNumeric,
                PostCodeRegex = countryInfo.PostCodeRegex,
                CurrencyName = countryInfo.CurrencyName,
                PhonePrefix = countryInfo.PhonePrefix,
                TopLevelDomain = countryInfo.TopLevelDomain,
                Languages = countryInfo.Languages
            };
            var readOnlyList = DeserializeEntry(entry,country);
            country.States = readOnlyList;
            return country;
        }

        public static void LoadAll()
        {
            using var stream = assembly.GetManifestResourceStream("CountryData.postcodes.zip")!;
            using ZipArchive archive = new(stream);
            foreach (var entry in archive.Entries)
            {
                var countryCode = entry.Name.Split('.').First();
                cache[countryCode] = ConstructCountry(entry, countryCode);
            }
        }

        static List<State> DeserializeEntry(ZipArchiveEntry entry, Country country)
        {
            using var entryStream = entry.Open();
            return DeserializeStates(country, entryStream).ToList();
        }

        static IEnumerable<State> DeserializeStates(Country country, Stream entryStream)
        {
            foreach (var state in Serializer.Deserialize<List<State>>(entryStream))
            {
                state.Country = country;
                foreach (var province in state.Provinces)
                {
                    ((Province)province).State = state;
                    foreach (var community in province.Communities)
                    {
                        ((Community)community).Province = province;
                        foreach (var place in community.Places)
                        {
                            ((Place)place).Community = community;
                        }
                    }
                }
                yield return state;
            }
        }
    }
}