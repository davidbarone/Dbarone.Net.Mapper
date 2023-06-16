namespace Dbarone.Net.Mapper;

public class CustomerWithDifferentTypesA {
    public int CustomerId { get; set; }
    public string CustomerDOB { get; set; }
    public string CustomerRank { get; set; }
}

public class CustomerWithDifferentTypesB {
    public int CustomerId { get; set; }
    public DateTime CustomerDOB { get; set; }
    public int CustomerRank { get; set; }
}
