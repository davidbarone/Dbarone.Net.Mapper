namespace Dbarone.Net.Mapper;
using System.Linq.Expressions;

public interface ITypeConverter
{
    public object? Convert(object? obj);
}

public class TypeConverter<T, U> : ITypeConverter
{
    public Func<T, U> converter;

    public TypeConverter(Func<T, U> converter)
    {
        this.converter = converter;
    }


    public object? Convert(object? obj)
    {
        return (object)converter.Invoke((T?)obj);
    }
}