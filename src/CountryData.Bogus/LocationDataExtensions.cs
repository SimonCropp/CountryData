using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;
using Bogus.Premium;

namespace CountryData.Bogus
{
    public static class LocationDataExtensions
    {
        public static CountryDataSet Country(this Faker faker)
        {
            return ContextHelper.GetOrSet(faker, () => new CountryDataSet());
        }

        static Random random = new();

        public static IState State(this ICountry country)
        {
            return country.States.Random();
        }

        public static IEnumerable<IState> States(this ICountry country, uint count)
        {
            return country.States.Random(count);
        }

        public static IProvince Province(this ICountry country)
        {
            return AllProvinces(country).ToList().Random();
        }

        public static IEnumerable<IProvince> Provinces(this ICountry country, uint count)
        {
            return AllProvinces(country).ToList().Random(count);
        }

        public static ICommunity Community(this ICountry country)
        {
            return AllCommunities(country).ToList().Random();
        }

        public static IEnumerable<ICommunity> Communities(this ICountry country, uint count)
        {
            return AllCommunities(country).ToList().Random(count);
        }

        public static IPlace Place(this ICountry country)
        {
            return AllPlaces(country).ToList().Random();
        }

        public static IEnumerable<IPlace> Places(this ICountry country, uint count)
        {
            return AllPlaces(country).ToList().Random(count);
        }
        public static string PostCode(this ICountry country)
        {
            return Place(country).PostCode;
        }

        public static IEnumerable<string> PostCode(this ICountry country, uint count)
        {
            return Places(country, count).Select(x=>x.PostCode);
        }

        public static IEnumerable<ICommunity> AllCommunities(this ICountry country)
        {
            return AllProvinces(country).SelectMany(x => x.Communities);
        }

        public static IEnumerable<IPlace> AllPlaces(this ICountry country)
        {
            return AllCommunities(country).SelectMany(x => x.Places);
        }

        public static IEnumerable<IProvince> AllProvinces(this ICountry country)
        {
            return country.States.SelectMany(x => x.Provinces);
        }

        static T Random<T>(this IReadOnlyList<T> source)
        {
            var r = random.Next(source.Count);
            return source[r];
        }

        static IEnumerable<T> Random<T>(this IReadOnlyList<T> source, uint count)
        {
            for (var i = 0; i < count; i++)
            {
                yield return source.Random();
            }
        }
    }
}