static class CountrySerializer
{
    public static List<State> Serialize(string country, List<PostCodeRow> rows, string directory)
    {
        var states = new List<State>();
        foreach (var stateGroup in rows.OrderBy(_ => _.StateCode).GroupBy(_ => _.State))
        {
            var provinceRows = stateGroup.ToList();
            var state = new State
            {
                Name = stateGroup.Key!,
                Code = provinceRows.First().StateCode
            };
            states.Add(state);

            var provinces = new List<Province>();
            foreach (var provinceGroup in provinceRows.OrderBy(_ => _.ProvinceCode).GroupBy(_ => _.Province))
            {
                var communityRows = provinceGroup.ToList();
                var province = new Province
                {
                    Name = provinceGroup.Key!,
                    Code = communityRows.First().ProvinceCode
                };
                provinces.Add(province);

                var communities = new List<Community>();
                foreach (var communityGroup in communityRows.OrderBy(_ => _.CommunityCode).GroupBy(_ => _.Community))
                {
                    var placeRows = communityGroup.ToList();
                    var community = new Community
                    {
                        Name = communityGroup.Key!,
                        Code = placeRows.First().CommunityCode
                    };
                    communities.Add(community);

                    var places = new List<Place>();
                    foreach (var place in placeRows.OrderBy(_ => _.PlaceName))
                    {
                        var item = new Place
                        {
                            PostCode = place.PostalCode,
                            Location = place.Location,
                            Name = place.PlaceName
                        };

                        places.Add(item);
                    }

                    community.Places = places;
                    community.PostCode = RollupPostcodes(places.Select(_ => _.PostCode));
                }

                province.Communities = communities;
                province.PostCode = RollupPostcodes(communities.Select(_ => _.PostCode));
            }
            state.Provinces = provinces;

            state.PostCode = RollupPostcodes(provinces.Select(_ => _.PostCode));
        }

        var path = Path.Combine(directory, country + ".json.txt");
        JsonSerializer.Serialize(states, path);
        return states;
    }

    static string? RollupPostcodes(IEnumerable<string?> postcodes)
    {
        // ReSharper disable PossibleMultipleEnumeration
        var firstPostCode = postcodes.First();
        if (postcodes.All(_ => _ == firstPostCode))
        {
            return firstPostCode;
        }

        return null;
    }
}