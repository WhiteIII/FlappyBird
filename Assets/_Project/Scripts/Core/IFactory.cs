namespace Project.Core
{
    public interface IFactory<T>
    {
        T Create();
    }
}
