using Dbarone.Net.Mapper;

public interface IMapperProvider {

    /// <summary>
    /// When implemented in a class, should return true if the class can map from-to type.
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="builder"></param>
    /// <returns></returns>
    public bool CanCreateMapFor(BuildType from, BuildType to, MapperBuilder builder);

    /// <summary>
    /// When implemented in a class, should return a <see cref="MapperDelegate"/> object that
    /// can map an object of 'from' type to 'to' type. This method should also perform any
    /// build-time validation, and add any errors to the errors collection.
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="builder"></param>
    /// <param name="path"></param>
    /// <param name="errors"></param>
    /// <returns></returns>
    public MapperDelegate GetMapFor(BuildType from, BuildType to, MapperBuilder builder, string path, List<MapperBuildError> errors);
}
