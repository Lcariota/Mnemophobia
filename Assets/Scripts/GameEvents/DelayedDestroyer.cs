using UnityEngine;
using System.Collections;


//For collectable objects to trigger
public class DelayedDestroyer : MonoBehaviour
{
    public void DestroyAfter(float delay)
    {
        StartCoroutine(DestroyAfterDelay(delay));
    }

    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}