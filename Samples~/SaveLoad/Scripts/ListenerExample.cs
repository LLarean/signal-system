using UnityEngine;
using UnityEngine.UI;

namespace GameSignals.Samples
{
    /// <summary>
    /// Example component demonstrating how to handle save/load signals.
    /// Updates UI text when events are received.
    /// </summary>
    public class ListenerExample : MonoBehaviour, IQuickSaveLoadHandler
    {
        [SerializeField] private Text _label;

        private void Start()
        {
            if (_label != null)
                _label.text = "The events did not invoked";
        }

        private void OnEnable()
        {
            SignalSystem.Subscribe(this);
        }

        private void OnDisable()
        {
            SignalSystem.Unsubscribe(this);
        }

        public void HandleSave()
        {
            if (_label != null)
                _label.text = "The <color=#005500><b>SAVE</b></color> event has invoked";
        }

        public void HandleLoad()
        {
            if (_label != null)
                _label.text = "The <color=#0000FF><b>LOAD</b></color> event has invoked";
        }
    }
}