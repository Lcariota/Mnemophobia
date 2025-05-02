using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class InspectorUI : MonoBehaviour
{
    [SerializeField] private GameObject inspectorPanel; // Panel to show details
    [SerializeField] private TMP_Text titleText; // Title 
    [SerializeField] private Image iconImage; // Item icon
    [SerializeField] private TMP_Text descriptionText; // Description

    private Dictionary<string, string> itemDescriptions = new Dictionary<string, string>
    {
        // Key Descriptions
        { "Key1", "A rusty key found in the basement. It looks old but still functional." },
        { "Key2", "The front door key... Maybe I can finally get away from this mess." },

        // Page Descriptions
        { "DiaryBook", "Olivia's Diary... All the pages seem to have been ripped out." },
        { "Page2", "A hastily scribbled note. Fear and urgency emanate from the words." }
    };

    public void ShowInspector(string itemTitle, Sprite itemSprite, string itemTag)
    {
        titleText.text = itemTitle;
        iconImage.sprite = itemSprite;
        iconImage.color = Color.white;

        // Set description based on itemTag
        if (itemDescriptions.TryGetValue(itemTag, out string description))
        {
            descriptionText.text = description;
        }
        else
        {
            descriptionText.text = "No description available.";
        }

        inspectorPanel.SetActive(true); 
    }

    public void HideInspector()
    {
        inspectorPanel.SetActive(false); 
    }
}
