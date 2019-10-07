using CountryData;

class PostCodeRow
{
    public string CountryCode = null!;
    public string PostalCode = null!;
    public string? PlaceName; //nullable

    public string? State; //nullable
    public string? StateCode; //nullable

    public string? Province; //nullable
    public string? ProvinceCode; //nullable

    public string? Community; //nullable
    public string? CommunityCode; //nullable

    public Location Location = null!;
    public ushort? Accuracy;
}