using UnityEngine;
using UnityEngine.UI;

namespace EventBusSystem.Samples
{
    public class EventCallerExample : MonoBehaviour
    {
        [SerializeField] private Button _save;
        [SerializeField] private Button _load;

        private void Start()
        {
            _save.onClick.AddListener(RaiseSaveEvent);
            _load.onClick.AddListener(RaiseLoadEvent);
        }

        private void RaiseLoadEvent()
        {
            EventBus.RaiseEvent<IQuickSaveLoadHandler>(handler => handler.HandleLoad());
        }

        private void RaiseSaveEvent()
        {
            EventBus.RaiseEvent<IQuickSaveLoadHandler>(handler => handler.HandleSave());
        }
    }
}
