using UnityEngine;
using UnityEngine.SceneManagement;

public class DEMOLoadScene : MonoBehaviour
{
    [SerializeField] private string scene1 = "House_worn_night";
    [SerializeField] private string scene2 = "House2";
    [SerializeField] private float cooldownDuration = 2f; // CoolDown

    private bool isOnCooldown = false;

    private void OnTriggerEnter(Collider other)
    {
        if (isOnCooldown) return;

        if (other.CompareTag("Player"))
        {
            string currentScene = SceneManager.GetActiveScene().name;
            string nextScene = currentScene == scene1 ? scene2 : scene1;

            isOnCooldown = true;
            StartCoroutine(LoadSceneWithCooldown(nextScene));
        }
    }

    private System.Collections.IEnumerator LoadSceneWithCooldown(string sceneName)
    {
        yield return new WaitForSeconds(cooldownDuration);
        SceneManager.LoadScene(sceneName);
    }
}
