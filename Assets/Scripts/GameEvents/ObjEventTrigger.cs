using UnityEngine;
using System.Collections;

public class ObjEventTrigger : MonoBehaviour
{
    public GameEvent eventToTrigger; 
    public float delay = 0f;

    public void TriggerEvent()
    {
        StartCoroutine(DelayedTrigger());
    }

    private IEnumerator DelayedTrigger()
    {
        if (delay > 0f)
            yield return new WaitForSeconds(delay);

        if (eventToTrigger != null)
            eventToTrigger.TriggerEvent();
    }
}