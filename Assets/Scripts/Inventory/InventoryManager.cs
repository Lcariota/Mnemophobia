//on player object


using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Collections;


public class InventoryManager : MonoBehaviour
{
    [System.Serializable]
    public class ItemUI
    {
        public string itemTag;
        public TMP_Text itemText;  // TMP for item title
        public Image itemImage;    // Icon
        public Button itemButton;  // Button to inspect item
    }

    [System.Serializable]
    public class DiaryPage
    {
        public string itemTag;
        public GameObject pagePanel; // Panel to show when collected
    }

    [SerializeField] private List<ItemUI> itemUIElements;
    [SerializeField] private List<DiaryPage> diaryPages;
    
    private Dictionary<string, GameObject> diaryPagePanels = new Dictionary<string, GameObject>();
    private HashSet<string> collectedItems = new HashSet<string>();

    [SerializeField] private TMP_Text collectedText;
    [SerializeField] private CanvasGroup collectedTextCanvasGroup;
    [SerializeField] private GameObject menuPanel;

    private Coroutine fadeCoroutine;

    private Coroutine hideCollectedTextCoroutine;

    private void Awake()
    {
        // Populate the dictionary with diary page panels
        foreach (var page in diaryPages)
        {
            if (page.pagePanel != null)
            {
                diaryPagePanels[page.itemTag] = page.pagePanel;
                page.pagePanel.SetActive(false);  
            }
            else
            {
                Debug.LogWarning($"Diary page panel missing for tag: {page.itemTag}");
            }
        }
    }

    public void AddItem(string itemTag, string itemTitle, Sprite itemSprite)
    {
        // Enable the diary page panel
        if (diaryPagePanels.TryGetValue(itemTag, out GameObject panel))
        {
            panel.SetActive(true);
        }

        // Handle item collection UI 
        if (!collectedItems.Contains(itemTag))
        {
            collectedItems.Add(itemTag);

            foreach (var itemUI in itemUIElements)
            {
                if (itemUI.itemTag == itemTag)
                {
                    if (itemUI.itemText != null)
                        itemUI.itemText.text = itemTitle;

                    if (itemUI.itemImage != null)
                    {
                        itemUI.itemImage.sprite = itemSprite;
                        itemUI.itemImage.color = Color.white;
                    }

                    if (itemUI.itemButton != null)
                    {
                        itemUI.itemButton.interactable = true;
                        itemUI.itemButton.onClick.AddListener(() =>
                        {
                            FindFirstObjectByType<InspectorUI>()?.ShowInspector(itemTitle, itemSprite, itemTag);
                        });
                    }

                    break;
                }
            }
        }
        else
        {
            Debug.Log($"Item '{itemTag}' already collected.");
        }

        ShowCollectedMessage($"{itemTitle} collected");
    }

    private void ShowCollectedMessage(string message)
    {
        if (collectedText == null || collectedTextCanvasGroup == null) return;

        collectedText.text = message;

        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        fadeCoroutine = StartCoroutine(FadeCollectedTextRoutine());
    }

    private IEnumerator FadeCollectedTextRoutine()
    {
        collectedTextCanvasGroup.alpha = 0;
        collectedTextCanvasGroup.gameObject.SetActive(true);

        // Fade in
        float duration = 0.3f;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            collectedTextCanvasGroup.alpha = Mathf.Lerp(0, 1, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        collectedTextCanvasGroup.alpha = 1;

        // Wait unless the menu opens
        float visibleTime = 3f;
        float timer = 0f;
        while (timer < visibleTime)
        {
            if (menuPanel != null && menuPanel.activeSelf)
            {
                collectedTextCanvasGroup.alpha = 0;
                yield break;
            }
            timer += Time.deltaTime;
            yield return null;
        }

        // Fade out
        elapsed = 0f;
        while (elapsed < duration)
        {
            collectedTextCanvasGroup.alpha = Mathf.Lerp(1, 0, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        collectedTextCanvasGroup.alpha = 0;
    }
}