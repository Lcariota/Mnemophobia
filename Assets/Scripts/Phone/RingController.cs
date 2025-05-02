using UnityEngine;

public class RingController : MonoBehaviour
{
    public AudioSource ringAudio;

    public void StartRinging()
    {
        if (ringAudio != null && !ringAudio.isPlaying)
        {
            ringAudio.Play();
        }
    }

    public void StopRinging()
    {
        if (ringAudio != null && ringAudio.isPlaying)
        {
            ringAudio.Stop();
        }
    }
}