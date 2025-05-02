using UnityEngine;
using UnityEngine.Audio;

public class RainManager : MonoBehaviour
{
    public AudioMixerSnapshot indoorSnapshot;
    public AudioMixerSnapshot outdoorSnapshot;
    public AudioMixerSnapshot basementSnapshot;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("OutdoorZone"))
        {
            outdoorSnapshot.TransitionTo(1f);
        }
        else if (other.CompareTag("BasementZone"))
        {
            basementSnapshot.TransitionTo(1f);
        }
        else if (other.CompareTag("IndoorZone"))
        {
            indoorSnapshot.TransitionTo(1f);
        }

    }

}