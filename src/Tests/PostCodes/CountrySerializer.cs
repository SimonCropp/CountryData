using System.Collections.Generic;
using System.IO;
using System.Linq;
using CountryData;

static class CountrySerializer
{
    public static List<State> Serialize(string country, List<PostCodeRow> rows, string directory)
    {
        List<State> states = new();
        foreach (var stateGroup in rows.OrderBy(x => x.StateCode).GroupBy(x => x.State))
        {
            var provinceRows = stateGroup.ToList();
            State state = new()
            {
                Name = stateGroup.Key!,
                Code = provinceRows.First().StateCode
            };
            states.Add(state);

            List<Province> provinces = new();
            foreach (var provinceGroup in provinceRows.OrderBy(x => x.ProvinceCode).GroupBy(x => x.Province))
            {
                var communityRows = provinceGroup.ToList();
                Province province = new()
                {
                    Name = provinceGroup.Key!,
                    Code = communityRows.First().ProvinceCode
                };
                provinces.Add(province);

                List<Community> communities = new();
                foreach (var communityGroup in communityRows.OrderBy(x => x.CommunityCode).GroupBy(x => x.Community))
                {
                    var placeRows = communityGroup.ToList();
                    Community community = new()
                    {
                        Name = communityGroup.Key!,
                        Code = placeRows.First().CommunityCode
                    };
                    communities.Add(community);

                    List<Place> places = new();
                    foreach (var place in placeRows.OrderBy(x => x.PlaceName))
                    {
                        Place item = new()
                        {
                            PostCode = place.PostalCode,
                            Location = place.Location,
                            Name = place.PlaceName
                        };

                        places.Add(item);
                    }

                    community.Places = places;
                    community.PostCode = RollupPostcodes(places.Select(x => x.PostCode));
                }

                province.Communities = communities;
                province.PostCode = RollupPostcodes(communities.Select(x => x.PostCode));
            }
            state.Provinces = provinces;

            state.PostCode = RollupPostcodes(provinces.Select(x => x.PostCode));
        }

        var path = Path.Combine(directory, country + ".json.txt");
        JsonSerializer.Serialize(states, path);
        return states;
    }

    static string? RollupPostcodes(IEnumerable<string?> postcodes)
    {
        // ReSharper disable PossibleMultipleEnumeration
        var firstPostCode = postcodes.First();
        if (postcodes.All(x => x == firstPostCode))
        {
            return firstPostCode;
        }

        return null;
    }
}