namespace Dbarone.Net.Mapper.Tests;
using Dbarone.Net.Extensions;

public class CaseChangeMemberRenameStrategyTests
{
    [Fact]
    public void RenameMember_ShouldRenameCorrectly()
    {
        CaseChangeMemberRenameStrategy strategy = new CaseChangeMemberRenameStrategy(CaseType.SnakeCase, CaseType.LowerCase);
        var renamed = strategy.RenameMember("the_cat_sat_on_the_mat");
        Assert.Equal("thecatsatonthemat", renamed);
    }
}