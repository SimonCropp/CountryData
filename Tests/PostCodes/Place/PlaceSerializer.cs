using System.Collections.Generic;

namespace Place
{
    static class PlaceSerializer
    {
        public static void Serialize(string country, List<Row> rows, string directory)
        {
            var places = new List<Place>();
            foreach (var row in rows)
            {
                var place = new Place
                {
                    Name = row.PlaceName,
                    PostCode = row.PostalCode,
                    LatLong = $"{row.Latitude},{row.Longitude}",
                };
                places.Add(place);
            }

            Serializer.Serialize(directory, country, new Country {Places = places});
        }
    }
}