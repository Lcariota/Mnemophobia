using UnityEngine;
using TMPro;


[CreateAssetMenu(fileName = "Diary", menuName = "Interactions/Diary Item")]
public class DiaryCollectable : Interaction
{
    private static int collectedCount = 0;
    private static TextMeshProUGUI[] diaryTexts;

    [SerializeField] private int totalDiaries = 2;

    private void Start()
    {
        if (diaryTexts == null)
        {
            diaryTexts = new TextMeshProUGUI[totalDiaries];
            for (int i = 0; i < totalDiaries; i++)
            {
                string textName = "DiaryText" + (i + 1);
                GameObject textObject = GameObject.Find(textName);
                if (textObject != null)
                {
                    diaryTexts[i] = textObject.GetComponent<TextMeshProUGUI>();
                    diaryTexts[i].gameObject.SetActive(false); 
                }
                else
                {
                    Debug.LogError(textName + " UI element not found in the scene!");
                }
            }
        }
    }

    public override void Interact(GameObject interactor, GameObject interactable)
    {
        if (collectedCount < totalDiaries)
        {
            diaryTexts[collectedCount].gameObject.SetActive(true);
            collectedCount++;
            Destroy(interactable);
        }
        else
        {
            Debug.LogWarning("All diary pages have already been collected.");
        }
    }
}
