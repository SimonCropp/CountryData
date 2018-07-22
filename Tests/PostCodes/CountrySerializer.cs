using System.Collections.Generic;
using System.IO;
using System.Linq;
using CountryData;

static class CountrySerializer
{
    public static List<State> Serialize(string country, List<PostCodeRow> rows, string directory)
    {
        var states = new List<State>();
        foreach (var stateGroup in rows.GroupBy(x => x.State))
        {
            var provinceRows = stateGroup.ToList();
            var state = new State
            {
                Name = stateGroup.Key,
                Code = provinceRows.First().StateCode
            };
            states.Add(state);

            var provinces = new List<Province>();
            foreach (var provinceGroup in provinceRows.GroupBy(x => x.Province))
            {
                var communityRows = provinceGroup.ToList();
                var province = new Province
                {
                    Name = provinceGroup.Key,
                    Code = communityRows.First().ProvinceCode
                };
                provinces.Add(province);

                var communities = new List<Community>();
                foreach (var communityGroup in communityRows.GroupBy(x => x.Community))
                {
                    var placeRows = communityGroup.ToList();
                    var community = new Community
                    {
                        Name = communityGroup.Key,
                        Code = placeRows.First().CommunityCode
                    };
                    communities.Add(community);

                    var places = new List<Place>();
                    foreach (var place in placeRows)
                    {
                        var item = new Place
                        {
                            PostCode = place.PostalCode,
                            LatLong = $"{place.Latitude},{place.Longitude}",
                            Name = place.PlaceName
                        };

                        places.Add(item);
                    }
                    community.Places = places;
                }
                province.Communities = communities;
                state.Provinces= provinces;
            }
        }

        var path = Path.Combine(directory, country + ".json.txt");
        JsonSerializer.Serialize(states, path);
        return states;
    }
}