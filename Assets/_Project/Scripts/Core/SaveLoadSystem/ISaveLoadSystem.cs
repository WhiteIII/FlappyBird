namespace Project.Core.SaveLoad
{
    public interface ISaveLoadSystem<T>
    {
        void Save(T data);
        public T Load();
    }
}
