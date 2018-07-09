using System.Collections.Generic;
using System.Linq;

namespace StateProvinceCommunity
{
    static class StateProvinceCommunitySerializer
    {
        public static void Serialize(string country, List<Row> rows, string directory)
        {
            var states = new List<State>();
            foreach (var stateGroup in rows.GroupBy(x => x.State))
            {
                var provinces = stateGroup.ToList();
                var state = new State
                {
                    Name = stateGroup.Key,
                    Code = provinces.First().StateCode
                };
                states.Add(state);

                foreach (var provinceGroup in provinces.GroupBy(x => x.Province))
                {
                    var communities = provinceGroup.ToList();
                    var province = new Province
                    {
                        Name = provinceGroup.Key,
                        Code = communities.First().ProvinceCode
                    };
                    state.Provinces.Add(province);

                    foreach (var communityGroup in communities.GroupBy(x => x.Community))
                    {
                        var places = communityGroup.ToList();
                        var community = new Community
                        {
                            Name = communityGroup.Key,
                            Code = places.First().CommunityCode
                        };
                        province.Communities.Add(community);
                        foreach (var place in places)
                        {
                            var item = new Place
                            {
                                PostCode = place.PostalCode,
                                LatLong = $"{place.Latitude},{place.Longitude}"
                            };
                            if (place.PlaceName != community.Name)
                            {
                                item.Name = place.PlaceName;
                            }

                            community.Places.Add(item);
                        }
                    }
                }
            }

            JsonSerializer.Serialize(directory, country, new Country {States = states});
        }
    }
}