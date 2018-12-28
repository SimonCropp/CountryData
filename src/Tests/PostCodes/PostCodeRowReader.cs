﻿using System.Collections.Generic;
using Xunit;

static class PostCodeRowReader
{
    public static IEnumerable<PostCodeRow> ReadRows(string allCountriesTxtPath)
    {
        foreach (var split in RowReader.ReadRows(allCountriesTxtPath))
        {
            var row = new PostCodeRow
            {
                CountryCode = split[0],
                PostalCode = split[1],
                PlaceName = split[2],
                State = split[3],
                StateCode = split[4],
                Province = split[5],
                ProvinceCode = split[6],
                Community = split[7],
                CommunityCode = split[8],
                Latitude = double.Parse(split[9]),
                Longitude = double.Parse(split[10]),
            };
            Assert.NotEmpty(row.PostalCode);
            var accuracy = split[11];
            if (accuracy != null)
            {
                row.Accuracy = ushort.Parse(accuracy);
            }

            yield return row;
        }
    }
}