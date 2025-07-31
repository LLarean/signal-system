namespace GameSignals.Samples
{
    /// <summary>
    /// Provides contract for objects handling quick save/load signals.
    /// </summary>
    public interface IQuickSaveLoadHandler : IGlobalSubscriber
    {
        /// <summary>
        /// Called when a save signal is raised.
        /// </summary>
        void HandleSave();
        
        /// <summary>
        /// Called when a load signal is raised.
        /// </summary>
        void HandleLoad();
    }
}