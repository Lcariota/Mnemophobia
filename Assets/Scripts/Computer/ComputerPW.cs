using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ComputerPW : MonoBehaviour
{
    public TMP_InputField passwordInputField;
    public TMP_Text errorMessage;
    public TMP_Text hintMessage; 
    public GameObject accessPanel;

    private string correctPassword = "AndrewToxic"; //Computer Password
    private bool hasAccess = false;
    private int failedAttempts = 0;

    void Start()
    {
        errorMessage.gameObject.SetActive(false);
        hintMessage.gameObject.SetActive(false);
        accessPanel.SetActive(false);
    }

    public void CheckPassword()
    {
        if (hasAccess)
        {
            accessPanel.SetActive(true);
            return;
        }

        // Normalize the input
        string normalizedInput = passwordInputField.text.Replace(" ", "").ToLower();
        string normalizedCorrectPassword = correctPassword.Replace(" ", "").ToLower();

        if (normalizedInput == normalizedCorrectPassword)
        {
            hasAccess = true;
            accessPanel.SetActive(true);
            errorMessage.gameObject.SetActive(false);
            hintMessage.gameObject.SetActive(false);
        }
        else
        {
            failedAttempts++;
            errorMessage.gameObject.SetActive(true);

            if (failedAttempts >= 3)
            {
                hintMessage.gameObject.SetActive(true);
            }
        }
    }
}