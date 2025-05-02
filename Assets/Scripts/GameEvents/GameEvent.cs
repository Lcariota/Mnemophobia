using UnityEngine;
using UnityEngine.Events;

public class GameEvent : MonoBehaviour
{
    public UnityEvent onTrigger;

    public void TriggerEvent()
    {
        onTrigger.Invoke();
    }
}