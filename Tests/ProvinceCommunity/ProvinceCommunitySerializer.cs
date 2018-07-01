using System;
using System.Collections.Generic;
using System.Linq;

namespace ProvinceCommunity
{
    static class ProvinceCommunitySerializer
    {
        public static void Serialize(List<Row> rows, Action<object> action)
        {
            var provinces = new List<Province>();
            foreach (var provinceGroup in rows.GroupBy(x => x.Province))
            {
                var communities = provinceGroup.ToList();
                var province = new Province
                {
                    Name = provinceGroup.Key,
                    Code = communities.First().ProvinceCode
                };
                provinces.Add(province);

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
                            LatLong = $"{place.Latitude},{place.Longitude}",
                            Accuracy = place.Accuracy,
                        };
                        if (place.PlaceName != community.Name)
                        {
                            item.Name = place.PlaceName;
                        }

                        community.Places.Add(item);
                    }
                }
            }

            action(new Country { Provinces = provinces });
        }
    }
}