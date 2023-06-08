
/// <summary>
/// Builds a mapping rule.
/// </summary>
/// <typeparam name="T"></typeparam>
/// <typeparam name="U"></typeparam>
public class MapperBuilder<T, U> {

    /// <summary>
    /// Creates a new MapperBuilder class.
    /// </summary>
    /// <returns></returns>
    public MapperBuilder<T,U> Create() {
        return new MapperBuilder<T, U>();
    }

    /// <summary>
    /// Sets the member naming convention for the source.
    /// </summary>
    /// <param name="convention"></param>
    /// <returns></returns>
    public MapperBuilder<T,U> SetSourceMemberNamingConvention(NamingConvention convention) {
        this.Configuration.SourceMemberNamingConvention = convention;
        return this;
    }

    public MapperBuilder<T, U> SetDestinationMemberNamingConvention(NamingConvention convention) {
        this.Configuration.DestinationMemberNamingConvention = convention;
        return this;
    }

    public EntityMap Configuration { get; } = new EntityMap();
}