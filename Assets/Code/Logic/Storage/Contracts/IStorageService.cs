namespace Code.Logic.Storage.Contracts
{
    public interface IStorageService
    {
        void Initialize();
        void SaveAll();
    }
}