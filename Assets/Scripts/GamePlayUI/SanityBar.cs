using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class SanityBar : MonoBehaviour
{
    [SerializeField] private Slider sanitySlider;
    [SerializeField] private Slider secondSanitySlider;
    [SerializeField] private TMP_Text sanityText;

    [SerializeField] private AudioSource sanityAudioSource;

    [SerializeField] private AudioClip lowSanityClip50;
    [SerializeField] private AudioClip lowSanityClip25;
    [SerializeField] private AudioClip lowSanityClip10;

    private bool played50 = false;
    private bool played25 = false;
    private bool played10 = false;

    private float sanity = 1000f;
    private bool isPraying = false;
    private Coroutine prayCoroutine;
    private Coroutine drainCoroutine;

    private void Start()
    {
        sanitySlider.maxValue = 1000f;
        secondSanitySlider.maxValue = 1000f;
        sanitySlider.value = sanity;
        secondSanitySlider.value = sanity;
        UpdateSanityText();

        drainCoroutine = StartCoroutine(DrainSanity());
    }

    private IEnumerator DrainSanity()
    {
        yield return new WaitForSeconds(300f);
        
        while (true)
        {
            if (!isPraying)
            {
                sanity -= 2f;
                sanity = Mathf.Clamp(sanity, 0, 1000);
                UpdateSanityUI();
            }
            yield return new WaitForSeconds(1f);
        }
    }

    public void StartPraying()
    {
        if (prayCoroutine == null)
        {
            isPraying = true;
            if (drainCoroutine != null)
            {
                StopCoroutine(drainCoroutine);
                drainCoroutine = null;
            }
            prayCoroutine = StartCoroutine(RegainSanity());
        }
    }

    public void StopPraying()
    {
        isPraying = false;
        if (prayCoroutine != null)
        {
            StopCoroutine(prayCoroutine);
            prayCoroutine = null;
        }

        StartCoroutine(DelayedSanityDrain());
    }

    private IEnumerator RegainSanity()
    {
        while (isPraying)
        {
            sanity += 10f;
            sanity = Mathf.Clamp(sanity, 0, 1000);
            UpdateSanityUI();
            yield return new WaitForSeconds(1f);
        }
    }

    private IEnumerator DelayedSanityDrain()
    {
        yield return new WaitForSeconds(60f);
        if (!isPraying)
        {
            drainCoroutine = StartCoroutine(DrainSanity());
        }
    }

    public void RestoreSanity(float amount)
    {
        sanity += amount;
        sanity = Mathf.Clamp(sanity, 0, 1000);
        UpdateSanityUI();
    }

    private void UpdateSanityUI()
    {
        sanitySlider.value = sanity;
        secondSanitySlider.value = sanity;
        UpdateSanityText();
        CheckSanityThresholds();
    }

    private void CheckSanityThresholds()
    {
        float sanityPercent = sanity / 1000f;

        if (sanityPercent <= 0.10f && !played10)
        {
            PlaySanityWarning(lowSanityClip10, 1.5f);
            played10 = true;
        }
        else if (sanityPercent <= 0.25f && !played25)
        {
            PlaySanityWarning(lowSanityClip25, 1.25f);
            played25 = true;
        }
        else if (sanityPercent <= 0.5f && !played50)
        {
            PlaySanityWarning(lowSanityClip50, 1f);
            played50 = true;
        }

        // Reset flags
        if (sanityPercent > 0.5f) played50 = played25 = played10 = false;
        else if (sanityPercent > 0.25f) played25 = played10 = false;
        else if (sanityPercent > 0.10f) played10 = false;
    }
    private void PlaySanityWarning(AudioClip clip, float pitch)
    {
        if (sanityAudioSource != null && clip != null)
        {
            sanityAudioSource.pitch = pitch;
            sanityAudioSource.spatialBlend = 1f; // Fully 3D
            sanityAudioSource.spread = 360f;
            sanityAudioSource.minDistance = 1f;
            sanityAudioSource.maxDistance = 15f;
            sanityAudioSource.rolloffMode = AudioRolloffMode.Linear;
            sanityAudioSource.PlayOneShot(clip);
        }
    }

    private void UpdateSanityText()
    {
        sanityText.text = $"{sanity:F0}/1000";
    }
}
