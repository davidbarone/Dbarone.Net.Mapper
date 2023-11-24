using System.Collections;
using System.Collections.ObjectModel;
using Dbarone.Net.Extensions;
using Dbarone.Net.Mapper;
using System.Collections.Generic;
using System.Linq;

namespace Dbarone.Net.Mapper;

/// <summary>
/// Maps types that implement IEnumerable or have sequences of elements. 
/// </summary>
public class EnumerableMapperOperator : MapperOperator
{
    public EnumerableMapperOperator(MapperBuilder builder, BuildType from, BuildType to, MapperOperator parent = null) : base(builder, from, to, parent)
    {
    }

    protected override IDictionary<string, MapperOperator> GetChildren()
    {
        // Children
        Dictionary<string, MapperOperator> children = new Dictionary<string, MapperOperator>();
        var fromElementType = From.EnumerableElementType;
        var toElementType = To.EnumerableElementType;
        var elementMapping = Builder.GetMapper(new SourceDestination(fromElementType, toElementType), this);
        children["[]"] = elementMapping;
        return children;
    }

    public override int Priority => 50;

    public override bool CanMap()
    {
        return To.Type.IsEnumerableType() && From.Type.IsEnumerableType();
    }

     public override MapperDelegate GetMap()
    {
        MapperDelegate mapping = (s, d) =>
            {
                var arr = (s as IEnumerable);
                EnumerableBuffer buffer = new EnumerableBuffer(arr, Children["[]"].GetMap());
                return buffer.To(To.Type);
            };

        return mapping;
    }
}