using System;
using System.Collections.Generic;
using System.Linq;

namespace State
{
    static class StateSerializer
    {
        public static void Serialize(List<Row> rows, Action<object> action)
        {
            var states = new List<State>();
            foreach (var stateGroup in rows.GroupBy(x => x.State))
            {
                var places = stateGroup.ToList();
                var state = new State
                {
                    Name = stateGroup.Key,
                    Code = places.First().StateCode
                };
                states.Add(state);

                foreach (var place in places)
                {
                    var item = new Place
                    {
                        PostCode = place.PostalCode,
                        LatLong = $"{place.Latitude},{place.Longitude}",
                        Accuracy = place.Accuracy,
                    };
                    if (place.PlaceName != state.Name)
                    {
                        item.Name = place.PlaceName;
                    }

                    state.Places.Add(item);
                }
            }

            action(new Country { States = states });
        }
    }
}