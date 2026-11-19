static class PostCodeRowReader
{
    public static IEnumerable<PostCodeRow> ReadRows(string allCountriesTxtPath)
    {
        foreach (var split in RowReader.ReadRows(allCountriesTxtPath))
        {
            var row = new PostCodeRow
            {
                CountryCode = split[0]!,
                PostalCode = split[1]!,
                PlaceName = split[2],
                State = split[3],
                StateCode = split[4],
                Province = split[5],
                ProvinceCode = split[6],
                Community = split[7],
                CommunityCode = split[8],
            };

            var s = split[9];
            if (s is not null)
            {
                row.Location = new()
                {
                    Latitude = double.Parse(s),
                    Longitude = double.Parse(split[10]!)
                };
            }

            if (row.PostalCode.Length == 0)
            {
                throw new($"Empty postal code for {row.CountryCode}");
            }

            var accuracy = split[11];
            if (accuracy is not null)
            {
                row.Accuracy = ushort.Parse(accuracy);
            }

            yield return row;
        }
    }
}