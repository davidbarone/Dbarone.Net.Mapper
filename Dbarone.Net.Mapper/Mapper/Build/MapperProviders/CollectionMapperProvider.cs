using System.Collections;
using System.Collections.ObjectModel;
using Dbarone.Net.Extensions;
using Dbarone.Net.Mapper;
using System.Collections.Generic;

/// <summary>
/// Maps types that implement IEnumerable. 
/// </summary>
public class EnumerableMapperProvider : IMapperProvider
{
    public bool CanCreateMapFor(BuildType from, BuildType to, MapperBuilder builder)
    {
        return to.Type.IsEnumerableType() && from.Type.IsEnumerableType();
    }

    public MapperDelegate GetMapFor(BuildType from, BuildType to, MapperBuilder builder, string path, List<MapperBuildError> errors)
    {
        MapperDelegate mapping = (s, d) =>
            {
                var creator = to.MemberResolver.CreateInstance(to.Type);
                var IEnumerableObj = s as IEnumerable;
                if (IEnumerableObj == null)
                {
                    throw new Exception("whoops");
                }
                else
                {
                    // ArrayList
                    if (to.Type == typeof(ArrayList))
                    {
                        var obj = (ArrayList)creator();
                        foreach (var element in IEnumerableObj)
                        {
                            obj.Add(element);
                        }
                        return obj;
                    }
                    // Array
                    else if (to.Type.IsArray) {
                        ArrayList obj = new ArrayList();
                        foreach (var element in IEnumerableObj)
                        {
                            obj.Add(element);
                        }
                        return obj.ToArray(to.Type);
                    }
                    else {
                        return null;
                    }
                }
            };

        return mapping;
    }
}