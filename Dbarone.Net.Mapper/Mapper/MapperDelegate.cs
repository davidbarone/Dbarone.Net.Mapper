using Dbarone.Net.Mapper;

/// <summary>
/// Delegate which handles mapping of source to target.
/// </summary>
/// <param name="source">The source object.</param>
/// <param name="target">The target object.</param>
/// <returns>The target value is returned.</returns>
public delegate object MapperDelegate(object source, object target);