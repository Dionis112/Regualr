namespace Regular.Console.Domain.Interfaces
{
    public interface ISerializer<T> where T : class
    {
        T Deserialize(string path);
        string Serialize(T item);
    }
}