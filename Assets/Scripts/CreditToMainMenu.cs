using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CreditToMainMenu : MonoBehaviour
{
    public Animator creditsAnimator;

    void Start()
    {
        StartCoroutine(WaitAndReturn());
    }

    IEnumerator WaitAndReturn()
    {
        // Wait until the credit animation finishes
        yield return new WaitForSeconds(creditsAnimator.GetCurrentAnimatorStateInfo(0).length);

        // Enable the cursor before switching to MainMenu
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        // Load the MainMenu scene
        SceneManager.LoadScene("MainMenu");
    }
}