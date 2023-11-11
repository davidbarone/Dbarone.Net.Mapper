using System.Collections;
using System.Dynamic;
using Dbarone.Net.Extensions;

namespace Dbarone.Net.Extensions;

/// <summary>
/// Buffer to convert an IEnumerable to other common IEnumerable types (generic and non-generic).
/// </summary>
public class EnumerableBuffer
{

    private int Count { get; init; }
    private object[] Buffer { get; init; }

    public T To<T>()
    {
        return (T)To(typeof(T));
    }

    public IEnumerable To(Type type)
    {
        if (type == typeof(ArrayList))
        {
            return ToArrayList();
        }
        else if (type == typeof(Queue))
        {
            return ToQueue();
        }
        else if (type == typeof(Stack))
        {
            return ToStack();
        }
        else if (type.IsArray)
        {
            var elementType = type.GetElementType();
            return ToArray(elementType);
        }
        else if (type.IsAssignableToGenericType(typeof(List<>)))
        {
            var elementType = type.GetElementType();
            return ToGenericList(elementType);
        }
        else if (type.IsAssignableToGenericType(typeof(IEnumerable<>)))
        {
            var elementType = type.GetElementType();
            return ToGenericIEnumerable(elementType);
        }
        throw new Exception("whoops");
    }

    public ArrayList ToArrayList()
    {
        return new ArrayList(this.Buffer);
    }

    public Queue ToQueue()
    {
        return new Queue(this.Buffer);
    }

    public Stack ToStack()
    {
        return new Stack(this.Buffer);
    }

    public List<T> ToGenericList<T>()
    {
        return this.Buffer.Cast<T>().ToList();
    }

    public IEnumerable ToGenericList(Type elementType)
    {
        // Get the cast method
        var castMethod = typeof(IEnumerable).GetExtensionMethods().First(m => m.Name == "Cast");
        // Get the cast method for the element type parameter, and invoke;
        var results = castMethod.MakeGenericMethod(elementType).Invoke(null, new object[] { this.Buffer });
        return results as IEnumerable;
    }

    public IEnumerable<T> ToGenericIEnumerable<T>()
    {
        return this.Buffer.Cast<T>();
    }

    public IEnumerable ToGenericIEnumerable(Type elementType)
    {
        var castMethod = typeof(IEnumerable).GetExtensionMethods().First(m => m.Name == "Cast");
        // Get the cast method for the element type parameter, and invoke;
        var results = castMethod.MakeGenericMethod(elementType).Invoke(null, new object[] { this.Buffer });
        return results as IEnumerable;
    }

    public IEnumerable ToArray<T>()
    {
        return this.Buffer.Cast<T>().ToArray<T>();
    }

    public IEnumerable ToArray(Type elementType)
    {
        var list = this.ToGenericList(elementType);

        var castMethod = typeof(IEnumerable<>).GetExtensionMethods().First(m => m.Name == "ToArray");
        // Get the cast method for the element type parameter, and invoke;
        var results = castMethod.MakeGenericMethod(elementType).Invoke(null, new object[] { list });
        return results as IEnumerable;
    }

    public EnumerableBuffer(IEnumerable source, MapperDelegate? mapper = null)
    {
        object[] buffer = null;
        int count = 0;
        ICollection collection = source as ICollection;
        bool isGeneric = source.GetType().IsGenericType;

        if (collection != null && mapper == null)
        {
            // We can use Count property
            count = collection.Count;
            if (count > 0)
            {
                buffer = new object[count];
                collection.CopyTo(buffer, 0);
            }
        }
        else
        {
            // We'll have to loop through items.
            foreach (var element in source)
            {
                if (buffer == null)
                {
                    buffer = new object[8];   // initialise, size=8
                }
                else if (buffer.Length == count)
                {
                    object[] newBuffer = new object[checked(count * 2)];
                    Array.Copy(buffer, 0, newBuffer, 0, count);
                    buffer = newBuffer;
                }
                var item = element;
                if (mapper != null)
                {
                    item = mapper(item, null);
                }

                buffer[count] = item;
                count++;
            }
        }

        if (count == 0)
        {
            this.Buffer = new object[0];
        }
        else if (count == buffer.Length)
        {
            this.Count = count;
            this.Buffer = buffer;
        }
        else
        {
            this.Count = count;
            object[] final = new object[count];
            Array.Copy(buffer, 0, final, 0, count);
            this.Buffer = final;
        }
    }
}