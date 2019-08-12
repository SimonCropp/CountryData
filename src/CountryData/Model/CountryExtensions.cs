using System.Collections.Generic;
using System.Linq;

namespace CountryData
{
    public static class CountryExtensions
    {
        public static IEnumerable<IProvince> Provinces(this ICountry country)
        {
            Guard.AgainstNull(country, nameof(country));
            return country.States.SelectMany(x => x.Provinces);
        }

        public static IEnumerable<ICommunity> Communities(this ICountry country)
        {
            Guard.AgainstNull(country, nameof(country));
            return country.Provinces().SelectMany(x => x.Communities);
        }

        public static IEnumerable<IPlace> Places(this ICountry country)
        {
            Guard.AgainstNull(country, nameof(country));
            return country.Communities().SelectMany(x => x.Places);
        }

        public static IReadOnlyDictionary<string, IEnumerator<IPlace>> PostCodes(this ICountry country)
        {
            Guard.AgainstNull(country, nameof(country));
            return country.Places()
                .GroupBy(x => x.PostCode)
                .ToDictionary(x => x.Key, x => x.GetEnumerator());
        }
    }
}