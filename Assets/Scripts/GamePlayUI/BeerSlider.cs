using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class BeerSlider : MonoBehaviour
{
    public static BeerSlider Instance { get; private set; }

    [SerializeField] private Slider beerSlider;
    [SerializeField] private Slider secondaryBeerSlider; //secondary slider
    [SerializeField] private TextMeshProUGUI sobrietyText;
    [SerializeField] private Image sliderFill;
    private float beerLevel = 50f;
    private int consecutiveBeers = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        beerSlider.minValue = 0;
        beerSlider.maxValue = 100;
        beerSlider.value = beerLevel;

        secondaryBeerSlider.minValue = 0; 
        secondaryBeerSlider.maxValue = 100;
        secondaryBeerSlider.value = beerLevel;

        StartCoroutine(NaturalDecay());
        UpdateSobrietyState();
    }

    private IEnumerator NaturalDecay()
    {
        yield return new WaitForSeconds(300f);
         
        while (true)
        {
            beerLevel -= 0.5f;
            beerLevel = Mathf.Clamp(beerLevel, 0, 100);
            beerSlider.value = beerLevel;
            secondaryBeerSlider.value = beerLevel; 
            UpdateSobrietyState();
            yield return new WaitForSeconds(1f);
        }
    }

    public void DrinkBeer()
    {
        consecutiveBeers++;
        beerLevel += 15f;
        if (consecutiveBeers >= 3)
        {
            beerLevel = 100f;
        }
        beerLevel = Mathf.Clamp(beerLevel, 0, 100);
        beerSlider.value = beerLevel;
        secondaryBeerSlider.value = beerLevel; 
        UpdateSobrietyState();
    }

    private void UpdateSobrietyState()
    {
        if (beerLevel >= 75)
        {
            sobrietyText.text = "Unstable";
            sliderFill.color = Color.red;
        }
        else if (beerLevel >= 25)
        {
            sobrietyText.text = "Normal";
            sliderFill.color = Color.green;
        }
        else
        {
            sobrietyText.text = "Withdraws";
            sliderFill.color = Color.red;
        }
    }
}
