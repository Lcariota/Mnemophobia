using Unity; 
using UnityEngine;


public class MoveFootsteps : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    public float speed = 2f;
    public AudioSource footstepSource;

    private bool isMoving = false;

    private void Start()
    {
        footstepSource.loop = false; 
        transform.position = startPoint.position;
    }

    private void Update()
    {
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPoint.position, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, endPoint.position) < 0.1f)
            {
                isMoving = false;
                if (footstepSource.isPlaying)
                {
                    footstepSource.Stop();
                }
            }
        }
    }

    public void StartFootstepMovement()
    {
        if (!isMoving)
        {
            isMoving = true;
            if (!footstepSource.isPlaying)
            {
                footstepSource.Play();
            }
        }
    }
}