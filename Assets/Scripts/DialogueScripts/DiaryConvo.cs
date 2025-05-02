using UnityEngine;
using System.Collections;


//Triggers Convo2 after picking up Diary
public class DiaryConvo : MonoBehaviour
{
    public GameEvent onDiaryPickedUp;  

    public float delayBeforePhone = 2f;

    void OnTriggerEnter(Collider other)
    {
        if (CompareTag("DiaryBook"))
        {
            StartCoroutine(DelayedTrigger());
        }
    }

    IEnumerator DelayedTrigger()
    {
        yield return new WaitForSeconds(delayBeforePhone);
        onDiaryPickedUp.TriggerEvent();
    }
}