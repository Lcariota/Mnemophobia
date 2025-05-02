using UnityEngine;
using TMPro;

public class CollectUI : MonoBehaviour
{
    public static CollectUI Instance { get; private set; }

    public TextMeshProUGUI beerText;
    public TextMeshProUGUI holyWaterText;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
}