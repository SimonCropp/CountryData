using System;
using System.Collections.Generic;
using System.Linq;

namespace StateCommunity
{
    static class StateCommunitySerializer
    {
        public static void Serialize(List<Row> rows, Action<object> action)
        {
            var states = new List<State>();
            foreach (var stateGroup in rows.GroupBy(x => x.State))
            {
                var communities = stateGroup.ToList();
                var state = new State
                {
                    Name = stateGroup.Key,
                    Code = communities.First().StateCode
                };
                states.Add(state);

                foreach (var communityGroup in communities.GroupBy(x => x.Community))
                {
                    var places = communityGroup.ToList();
                    var community = new Community
                    {
                        Name = communityGroup.Key,
                        Code = places.First().ProvinceCode
                    };
                    state.Communities.Add(community);

                    foreach (var place in places)
                    {
                        var item = new Place
                        {
                            PostCode = place.PostalCode,
                            LatLong = $"{place.Latitude},{place.Longitude}",
                        };
                        if (place.PlaceName != community.Name)
                        {
                            item.Name = place.PlaceName;
                        }

                        community.Places.Add(item);
                    }
                }
            }
            action(new Country { States = states });
        }
    }
}