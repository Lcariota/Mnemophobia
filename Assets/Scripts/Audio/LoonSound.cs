using UnityEngine;

public class LoonSound : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource; 
    [SerializeField] private AudioClip soundClip;      

    private float nextPlayTime;

    void Start()
    {
        ScheduleNextSound();
    }

    void Update()
    {
        if (Time.time >= nextPlayTime)
        {
            PlaySound();
            ScheduleNextSound();
        }
    }

    void PlaySound()
    {
        if (audioSource != null && soundClip != null)
        {
            audioSource.PlayOneShot(soundClip);
        }
        else
        {
            Debug.LogWarning("AudioSource or AudioClip not assigned.");
        }
    }

    void ScheduleNextSound()
    {
        float randomDelay = Random.Range(300f, 1200f); // 5 to 20 minutes in seconds
        nextPlayTime = Time.time + randomDelay;
    }
}