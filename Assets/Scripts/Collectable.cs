using UnityEngine;
using TMPro;

public enum CollectableType
{
    Beer,
    HolyWater
}

[CreateAssetMenu(fileName = "Collectable", menuName = "Interactions/Collect Item")]
public class Collectable : Interaction
{
    private static int[] collectedCounts = new int[2];

    public override void Interact(GameObject interactor, GameObject interactable)
    {
        CollectableType collectableType;
        
        if (interactable.CompareTag("Beer"))
        {
            collectableType = CollectableType.Beer;
        }
        else if (interactable.CompareTag("HolyWater"))
        {
            collectableType = CollectableType.HolyWater;
        }
        else
        {
            Debug.LogError("Unknown collectable tag: " + interactable.tag);
            return;
        }

        CollectItem(interactable, collectableType);
    }

    private void CollectItem(GameObject item, CollectableType collectableType)
    {
        collectedCounts[(int)collectableType]++;
        Debug.Log($"{collectableType} count is now {collectedCounts[(int)collectableType]}");

        UpdateUI(collectableType);
        Destroy(item);
    }

    private void UpdateUI(CollectableType collectableType)
    {
        if (CollectUI.Instance == null)
        {
            Debug.LogError("CollectUI is missing in the scene!");
            return;
        }

        TextMeshProUGUI text = collectableType == CollectableType.Beer
            ? CollectUI.Instance.beerText
            : CollectUI.Instance.holyWaterText;

        if (text != null)
        {
            text.text = collectedCounts[(int)collectableType].ToString();
            text.ForceMeshUpdate();
        }
        else
        {
            Debug.LogError($"{collectableType} TMP reference is NULL in CollectUI!");
        }
    }

    public static void UseItem(int typeIndex)
    {
        if (collectedCounts[typeIndex] > 0)
        {
            collectedCounts[typeIndex]--;
            Debug.Log($"Used item of type {(CollectableType)typeIndex}");

            if (CollectUI.Instance != null)
            {
                TextMeshProUGUI text = typeIndex == (int)CollectableType.Beer
                    ? CollectUI.Instance.beerText
                    : CollectUI.Instance.holyWaterText;

                if (text != null)
                {
                    text.text = collectedCounts[typeIndex].ToString();
                    text.ForceMeshUpdate();
                }
            }

            if ((CollectableType)typeIndex == CollectableType.Beer)
            {
                BeerSlider.Instance?.DrinkBeer();
            }
            else if ((CollectableType)typeIndex == CollectableType.HolyWater)
            {
                SanityBar sanityBar = GameObject.FindFirstObjectByType<SanityBar>();
                if (sanityBar != null)
                {
                    sanityBar.RestoreSanity(30f);
                }
                else
                {
                    Debug.LogError("SanityBar not found in the scene!");
                }
            }
        }
        else
        {
            Debug.Log($"No items of type {(CollectableType)typeIndex} left!");
        }
    }
}