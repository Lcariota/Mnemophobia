using UnityEngine;
using TMPro;
using System.Collections;

public class InspectManagerUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private TextMeshProUGUI inspectionText;
    [SerializeField] private float fadeDuration = 0.5f;
    [SerializeField] private float displayTime = 2f;

    private Coroutine _currentRoutine;

    public void ShowInspection(string text)
    {
        if (_currentRoutine != null)
            StopCoroutine(_currentRoutine);

        _currentRoutine = StartCoroutine(ShowRoutine(text));
    }

    private IEnumerator ShowRoutine(string text)
    {
        inspectionText.text = text;
        yield return FadeCanvasGroup(0, 1, fadeDuration);

        yield return new WaitForSeconds(displayTime);

        yield return FadeCanvasGroup(1, 0, fadeDuration);
    }

    private IEnumerator FadeCanvasGroup(float start, float end, float duration)
    {
        float time = 0f;
        while (time < duration)
        {
            time += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(start, end, time / duration);
            yield return null;
        }
        canvasGroup.alpha = end;
    }
}