namespace Dbarone.Net.Mapper.Tests;

public class CustomerEntity
{
    public int CustomerId { get; set; }
    public string Name { get; set; } = default!;
    public int AddressId { get; set; }
    public string Country { get; set; } = default!;
    public char Rating { get; set; }
}
