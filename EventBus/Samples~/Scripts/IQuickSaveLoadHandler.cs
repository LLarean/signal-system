namespace EventBus.Samples.Scripts
{
    public interface IQuickSaveLoadHandler : IGlobalSubscriber
    {
        void HandleSave();
        void HandleLoad();
    }
}