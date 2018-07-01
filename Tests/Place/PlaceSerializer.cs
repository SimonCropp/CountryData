using System;
using System.Collections.Generic;

namespace Place
{
    static class PlaceSerializer
    {
        public static void Serialize(List<Row> rows, Action<object> action)
        {
            var places = new List<Place>();
            foreach (var row in rows)
            {
                var place = new Place
                {
                    Name = row.PlaceName,
                    Accuracy = row.Accuracy,
                    LatLong = $"{row.Latitude},{row.Longitude}",
                };
                places.Add(place);
            }

            action(new Country { Places = places });
        }
    }
}