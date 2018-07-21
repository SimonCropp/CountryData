using System;
using System.Collections.Generic;
using System.Linq;

namespace CountryData.Bogus
{
    public static class LocationDataExtensions
    {
        static Random random = new Random();

        public static IState State(this IReadOnlyList<IState> states)
        {
            return states.Random();
        }

        public static IEnumerable<IState> States(this IReadOnlyList<IState> states, uint count)
        {
            return states.Random(count);
        }

        public static IProvince Province(this IReadOnlyList<IState> states)
        {
            return AllProvinces(states).ToList().Random();
        }

        public static IEnumerable<IProvince> Provinces(this IReadOnlyList<IState> states, uint count)
        {
            return AllProvinces(states).ToList().Random(count);
        }

        public static ICommunity Community(this IReadOnlyList<IState> states)
        {
            return AllCommunities(states).ToList().Random();
        }

        public static IEnumerable<ICommunity> Communities(this IReadOnlyList<IState> states, uint count)
        {
            return AllCommunities(states).ToList().Random(count);
        }

        public static IPlace Place(this IReadOnlyList<IState> states)
        {
            return AllPlaces(states).ToList().Random();
        }

        public static IEnumerable<IPlace> Places(this IReadOnlyList<IState> states, uint count)
        {
            return AllPlaces(states).ToList().Random(count);
        }

        public static IEnumerable<ICommunity> AllCommunities(this IReadOnlyList<IState> states)
        {
            return AllProvinces(states).SelectMany(x => x.Communities);
        }

        public static IEnumerable<IPlace> AllPlaces(this IReadOnlyList<IState> states)
        {
            return AllCommunities(states).SelectMany(x => x.Places);
        }

        public static IEnumerable<IProvince> AllProvinces(this IReadOnlyList<IState> states)
        {
            return states.SelectMany(x => x.Provinces);
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