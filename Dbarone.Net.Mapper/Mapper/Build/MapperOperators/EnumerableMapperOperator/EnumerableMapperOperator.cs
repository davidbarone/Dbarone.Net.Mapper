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
    /// <summary>
    /// Creates a new <see cref="EnumerableMapperOperator"/> instance. 
    /// </summary>
    /// <param name="builder">The <see cref="MapperBuilder"/> instance.</param>
    /// <param name="from">The From <see cref="BuildType"/> instance.</param>
    /// <param name="to">The To <see cref="BuildType"/> instance.</param>
    /// <param name="parent">An optional parent <see cref="MapperOperator"/> instance.</param>
    public EnumerableMapperOperator(MapperBuilder builder, BuildType from, BuildType to, MapperOperator? parent = null) : base(builder, from, to, parent)
    {
    }

    /// <summary>
    /// GetChildren implementation for <see cref="EnumerableMapperOperator"/>.
    /// </summary>
    /// <returns>Returns the children operators.</returns>
    /// <exception cref="MapperBuildException"></exception>
    protected override IDictionary<string, MapperOperator> GetChildren()
    {
        // Children
        Dictionary<string, MapperOperator> children = new Dictionary<string, MapperOperator>();
        var fromElementType = From.EnumerableElementType;
        var toElementType = To.EnumerableElementType;

        if (fromElementType == null)
        {
            throw new MapperBuildException(From.Type, MapperEndPoint.Source, "", "Element type is null.");
        }
        if (toElementType == null)
        {
            throw new MapperBuildException(To.Type, MapperEndPoint.Destination, "", "Element type is null.");
        }

        var elementMapping = Builder.GetMapper(new SourceDestination(fromElementType, toElementType), this);
        children["[]"] = elementMapping;
        return children;
    }

    /// <summary>
    /// Overrides the priority of the <see cref="EnumerableMapperOperator"/> instance.
    /// </summary>
    public override int Priority => 50;

    /// <summary>
    /// The <see cref="EnumerableMapperOperator"/> type is able to map when both From and To types are enumerable types. 
    /// </summary>
    /// <returns>Returns true when From and To types are enumerable types.</returns>
    public override bool CanMap()
    {
        return To.Type.IsEnumerableType() && From.Type.IsEnumerableType();
    }

    /// <summary>
    /// Returns the <see cref="MapperDelegate"/> object that performs the mapping. 
    /// </summary>
    /// <returns>Returns the <see cref="MapperDelegate"/> object that performs the mapping.</returns>
    public override MapperDelegate GetMap()
    {
        MapperDelegate mapping = (s, d) =>
            {
                var arr = (s as IEnumerable);
                if (arr == null)
                {
                    throw new MapperBuildException(From.Type, MapperEndPoint.Source, this.GetPath(), "Type does not implement IEnumerable.");
                }
                EnumerableBuffer buffer = new EnumerableBuffer(arr, Children["[]"].GetMap());
                return buffer.To(To.Type);
            };

        return mapping;
    }
}