using System;
using System.Collections.Generic;
using System.Linq;

namespace Province
{
    static class ProvinceSerializer
    {
        public static void Serialize(List<Row> rows, Action<object> action)
        {
            var provinces = new List<Province>();
            foreach (var provinceGroup in rows.GroupBy(x => x.Province))
            {
                var places = provinceGroup.ToList();
                var province = new Province
                {
                    Name = provinceGroup.Key,
                    Code = places.First().StateCode
                };
                provinces.Add(province);

                foreach (var place in places)
                {
                    var item = new Place
                    {
                        PostCode = place.PostalCode,
                        LatLong = $"{place.Latitude},{place.Longitude}",
                    };
                    if (place.PlaceName != province.Name)
                    {
                        item.Name = place.PlaceName;
                    }

                    province.Places.Add(item);
                }
            }

            action(new Country { Provinces = provinces });
        }
    }
}