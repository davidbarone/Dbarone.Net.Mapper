namespace Dbarone.Net.Mapper.Tests;

/// <summary>
/// This class has 8 field/property members in total:
/// - 2 * public properties
/// - 1 * private property
/// - 1 * public field
/// - 1 * private field
/// - 3 * private backing fields for properties 
/// </summary>
public class TypeWithPrivatePropertiesAndFields
{
    private int _customerId;
    private string _customerPrivateProperty = default!;
    private string _customerName = default!;

    /// <summary>
    /// CustomerId
    /// </summary>
    public int CustomerId
    {
        get
        {
            return _customerId;
        }
        set
        {
            _customerId = value;

        }
    }

    /// <summary>
    /// CustomerName
    /// </summary>
    public string CustomerName
    {
        get
        {
            return _customerName;
        }
        set
        {
            _customerName = value;
        }
    }

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
    private string CustomerPrivateProperty
    {
        get
        {
            return _customerPrivateProperty;
        }
        set
        {
            _customerPrivateProperty = value;
        }
    }
}