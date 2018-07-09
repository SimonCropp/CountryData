using System.Collections.Generic;
using System.Linq;

namespace StateProvince
{
    static class StateProvinceSerializer
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
                    var places = provinceGroup.ToList();
                    var province = new Province
                    {
                        Name = provinceGroup.Key,
                        Code = places.First().ProvinceCode
                    };
                    state.Provinces.Add(province);

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
            }

            Serializer.Serialize(directory, country, new Country {States = states});
        }
    }
}