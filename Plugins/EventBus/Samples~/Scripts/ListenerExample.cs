using UnityEngine;
using UnityEngine.UI;

namespace EventBus.Samples.Scripts
{
    public class ListenerExample : MonoBehaviour, IQuickSaveLoadHandler
    {
        [SerializeField] private Text _label;

        private void Start()
        {
            _label.text = "The events did not invoked";
        }
        
        private void OnEnable()
        {
            EventBus.Subscribe(this);
        }

        private void OnDisable()
        {
            EventBus.Unsubscribe(this);
        }

        public void HandleSave()
        {
            _label.text = "The <color=#005500><b>SAVE</b></color> event has invoked";
        }

        public void HandleLoad()
        {
            _label.text = "The <color=#0000FF><b>LOAD</b></color> event has invoked";
        }
    }
}
