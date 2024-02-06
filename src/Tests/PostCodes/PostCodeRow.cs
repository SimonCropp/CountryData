class PostCodeRow
{
    public string CountryCode = null!;
    public string PostalCode = null!;
    public string? PlaceName;

    public string? State;
    public string? StateCode;

    public string? Province;
    public string? ProvinceCode;

    public string? Community;
    public string? CommunityCode;

    public Location Location = null!;
    public ushort? Accuracy;
}