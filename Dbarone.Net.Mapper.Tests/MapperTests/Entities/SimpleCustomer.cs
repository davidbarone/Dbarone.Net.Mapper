namespace Dbarone.Net.Mapper;

/// <summary>
/// Simple customer entity.
/// </summary>
public class SimpleCustomer {
    public int CustomerId { get; set; }
    public string CustomerName { get; set; } = default!;
}