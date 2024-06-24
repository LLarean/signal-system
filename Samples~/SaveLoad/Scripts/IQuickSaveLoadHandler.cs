namespace EventBusSystem.Samples
{
    public interface IQuickSaveLoadHandler : IGlobalSubscriber
    {
        void HandleSave();
        void HandleLoad();
    }
}