using UnityEngine;
using UnityEngine.SceneManagement;

public class WakeUp : MonoBehaviour
{
    private Animator animator;

    public bool StartGame = false;

    void Start()
    {
        animator = GetComponent<Animator>();

        if (SceneManager.GetActiveScene().name == "House_worn_night")
        {
            StartGame = true;
            animator.SetBool("StartGame", true);
        }
    }
}