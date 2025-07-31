using UnityEngine;
using UnityEngine.UI;

namespace GameSignals.Samples
{
    /// <summary>
    /// Example component demonstrating how to raise signals for save/load operations.
    /// </summary>
    public class EventCallerExample : MonoBehaviour
    {
        [SerializeField] private Button _save;
        [SerializeField] private Button _load;

        private void Start()
        {
            if (_save != null) _save.onClick.AddListener(RaiseSaveEvent);
            if (_load != null) _load.onClick.AddListener(RaiseLoadEvent);
        }

        private void OnDestroy()
        {
            if (_save != null) _save.onClick.RemoveListener(RaiseSaveEvent);
            if (_load != null) _load.onClick.RemoveListener(RaiseLoadEvent);
        }

        /// <summary>
        /// Raises the load event via SignalSystem.
        /// </summary>
        private void RaiseLoadEvent()
        {
            SignalSystem.Raise<IQuickSaveLoadHandler>(handler => handler.HandleLoad());
        }

        /// <summary>
        /// Raises the save event via SignalSystem.
        /// </summary>
        private void RaiseSaveEvent()
        {
            SignalSystem.Raise<IQuickSaveLoadHandler>(handler => handler.HandleSave());
        }
    }
}
