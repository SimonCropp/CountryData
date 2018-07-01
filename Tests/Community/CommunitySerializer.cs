using System;
using System.Collections.Generic;
using System.Linq;

namespace Community
{
    static class CommunitySerializer
    {
        public static void Serialize(List<Row> rows, Action<object> action)
        {
            var communities = new List<Community>();
            foreach (var stateGroup in rows.GroupBy(x => x.Community))
            {
                var places = stateGroup.ToList();
                var province = new Community
                {
                    Name = stateGroup.Key,
                    Code = places.First().StateCode
                };
                communities.Add(province);

                foreach (var place in places)
                {
                    var item = new Place
                    {
                        PostCode = place.PostalCode,
                        LatLong = $"{place.Latitude},{place.Longitude}",
                        Accuracy = place.Accuracy,
                    };
                    if (place.PlaceName != province.Name)
                    {
                        item.Name = place.PlaceName;
                    }

                    province.Places.Add(item);
                }
            }

            action(new Country { Communities = communities });
        }
    }
}