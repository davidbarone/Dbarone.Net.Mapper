namespace Dbarone.Net.Mapper;

/// <summary>
/// Specifies the mode of a mapper diagnostic event.
/// </summary>
public enum MapperOperatorDiagnosticMode
{
    /// <summary>
    /// Diagnostic event occurs during build time.
    /// </summary>
    Build,

    /// <summary>
    /// Diagnostic event occurs during run time.
    /// </summary>
    Runtime
}
