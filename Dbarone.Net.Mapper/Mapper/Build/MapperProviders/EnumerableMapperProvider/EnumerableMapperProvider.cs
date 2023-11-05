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
public class EnumerableMapperProvider : IMapperProvider
{
    public bool CanCreateMapFor(BuildType from, BuildType to, MapperBuilder builder)
    {
        return to.Type.IsEnumerableType() && from.Type.IsEnumerableType();
    }

    public MapperDelegate GetMapFor(BuildType from, BuildType to, MapperBuilder builder, string path, List<MapperBuildError> errors)
    {
        var fromElementType = from.EnumerableElementType;
        var toElementType = to.EnumerableElementType;
        var elementMapping = builder.GetMapper(new SourceDestination(fromElementType, toElementType));

        MapperDelegate mapping = (s, d) =>
            {
                var arr = (s as IEnumerable);
                EnumerableBuffer buffer = new EnumerableBuffer(arr, elementMapping);
                return buffer.To(to.Type);
            };

        return mapping;
    }
}