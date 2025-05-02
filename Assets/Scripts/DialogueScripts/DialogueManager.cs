using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public CanvasGroup dialogueCanvasGroup;

    public float fadeDuration = 0.5f;
    public float waitBetweenBlocks = 0.2f;

    public DialogueBlock startingBlock;

    private string[] currentLines;
    private string[] currentNames;
    private int index;
    private bool isTransitioning;

    public UnityEvent onDialogueEnd;

    void Start()
    {
        if (startingBlock != null)
        {
            StartDialogue(startingBlock);
        }
    }

    public void StartDialogue(DialogueBlock block)
    {
        currentLines = block.lines;
        index = 0;
        StartCoroutine(FadeInBlock());
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1) && !isTransitioning && currentLines != null)
        {
            if (index < currentLines.Length - 1)
            {
                index++;
                StartCoroutine(FadeInBlock());
            }
            else
            {
                StartCoroutine(FadeOutDialogue());
                currentLines = null;
                onDialogueEnd.Invoke();
            }
        }
    }

    IEnumerator FadeInBlock()
    {
        isTransitioning = true;

        dialogueCanvasGroup.alpha = 0f;
        dialogueCanvasGroup.interactable = true;
        dialogueCanvasGroup.blocksRaycasts = true;

        dialogueText.text = currentLines[index];

        yield return new WaitForSeconds(waitBetweenBlocks);

        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            dialogueCanvasGroup.alpha = t / fadeDuration;
            yield return null;
        }

        dialogueCanvasGroup.alpha = 1f;
        isTransitioning = false;
    }

    IEnumerator FadeOutDialogue()
    {
        isTransitioning = true;

        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            dialogueCanvasGroup.alpha = 1f - (t / fadeDuration);
            yield return null;
        }

        dialogueCanvasGroup.alpha = 0f;
        dialogueCanvasGroup.interactable = false;
        dialogueCanvasGroup.blocksRaycasts = false;

        isTransitioning = false;
    }
}