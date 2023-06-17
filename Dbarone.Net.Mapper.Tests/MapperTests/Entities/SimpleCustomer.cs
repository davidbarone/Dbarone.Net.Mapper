namespace Dbarone.Net.Mapper.Tests;

/// <summary>
/// Simple customer entity.
/// </summary>
public class SimpleCustomer {
    public int CustomerId { get; set; }
    public string CustomerName { get; set; } = default!;
}