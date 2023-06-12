public class SimpleCustomerWithPrivatePropertiesAndFields
{
    /// <summary>
    /// CustomerId
    /// </summary>
    public int CustomerId { get; set; }

    /// <summary>
    /// CustomerName
    /// </summary>
    public string CustomerName { get; set; }

    /// <summary>
    /// Public field.
    /// </summary>
    public string CustomerCodeField;

    /// <summary>
    /// Private field.
    /// </summary>
    private string CustomerPrivateField;

    /// <summary>
    /// Private property.
    /// </summary>
    private string CustomerPrivateProperty { get; set; }
}