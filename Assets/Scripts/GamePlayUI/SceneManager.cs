using UnityEngine;
using UnityEngine.SceneManagement;

namespace StudioProject.Manager
{
    public class SceneHandler : MonoBehaviour
    {
        [SerializeField] private InputManager inputManager;

        // Loads the next scene in the build order
        public void LoadNextScene()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            int nextSceneIndex = currentSceneIndex + 1;

            if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(nextSceneIndex);
            }
            else
            {
                Debug.LogWarning("No next scene in build order.");
            }
        }

        // Loads a scene by its name
        public void LoadSceneByName(string sceneName)
        {
            if (!string.IsNullOrEmpty(sceneName))
            {
                SceneManager.LoadScene(sceneName);
            }
            else
            {
                Debug.LogWarning("Scene name is empty or null.");
            }
        }

        // Reloads the current active scene
        public void ReloadScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        // Quits the game application
        public void QuitGame()
        {
            Application.Quit();
            Debug.Log("Game is quitting...");
        }

        // Closes the PauseCanvas and resumes gameplay
        public void ClosePauseMenuButton()
        {
            if (inputManager != null)
            {
                inputManager.ForcePauseState(false, true); 
            }
        }
    }
}