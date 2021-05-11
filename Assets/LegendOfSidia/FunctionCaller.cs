using UnityEngine;
using UnityEngine.Events;

namespace LegendOfSidia
{
    public class FunctionCaller : MonoBehaviour
    {
        [System.Serializable]
        public struct EventData
        {
            public string name;
            public KeyCode key;
            public UnityEvent unityEvent;
        }

        public EventData[] events;

        private void Update()
        {
            foreach (EventData e in events)
            {
                if (Input.GetKeyDown(e.key))
                    e.unityEvent?.Invoke();
            }
        }
    }
}