
/// <summary>
/// Address class (used within CustomerWithAddressObject).
/// </summary>
public class CustomerChildAddress {
    public string AddressLine1 { get; set; }
    public string AddressLine2 { get; set; }
    public string City { get; set; }
}

/// <summary>
/// Test class with child class.
/// </summary>
public class CustomerWithChildAddress {
    public int CustomerId { get; set; }
    public string CustomerName { get; set; }
    public CustomerChildAddress Address { get; set; }
}