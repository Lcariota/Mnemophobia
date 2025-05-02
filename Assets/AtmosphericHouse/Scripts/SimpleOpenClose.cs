using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FS_Atmo
{
    public class SimpleOpenClose : MonoBehaviour
    {
        private Animator myAnimator;
        private Animator additionalAnimator;
        public bool objectOpen;
        public bool objectOpenAdditional;
        public GameObject animateAdditional;
        private bool hasAdditional = false;
        float myNormalizedTime;

        public AudioSource audioSource;
        public AudioClip openSound;
        public AudioClip closeSound;

        private Transform playerCamera;

        public bool isLocked = false; // Track locked status

        public delegate void DoorNeedsOpening(SimpleOpenClose door);
        public static event DoorNeedsOpening OnDoorShouldOpen;

        void Start()
        {
            myAnimator = GetComponent<Animator>() ?? GetComponentInParent<Animator>();
            if (audioSource == null)
            {
                audioSource = GetComponent<AudioSource>();
            }
            playerCamera = Camera.main.transform;

            if (objectOpen)
            {
                myAnimator.Play("Open", 0, 1.0f);
            }

            if (animateAdditional != null && animateAdditional.GetComponent<SimpleOpenClose>())
            {
                additionalAnimator = animateAdditional.GetComponent<Animator>();
                hasAdditional = true;
                objectOpenAdditional = animateAdditional.GetComponent<SimpleOpenClose>().objectOpen;
            }
            else
            {
                hasAdditional = false;
            }

            StartCoroutine(CheckForAutoOpen());
        }

        void ObjectClicked()
    {
        if (isLocked)
        {
            Debug.Log("Door is locked.");
            return;
        }

        myNormalizedTime = myAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime;

        if (myNormalizedTime >= 1.0)
        {
            if (hasAdditional)
                ToggleDoorWithAdditional();
            else
                ToggleDoor();
        }
    }

        void ToggleDoor()
        {
            if (objectOpen)
            {
                myAnimator.Play("Close", 0, 0.0f);
                objectOpen = false;
                PlaySound(closeSound);
            }
            else
            {
                myAnimator.Play("Open", 0, 0.0f);
                objectOpen = true;
                PlaySound(openSound);
            }
        }

        void ToggleDoorWithAdditional()
        {
            if (objectOpen)
            {
                myAnimator.Play("Close", 0, 0.0f);
                objectOpen = false;
                PlaySound(closeSound);
                animateAdditional.GetComponent<SimpleOpenClose>().objectOpenAdditional = false;

                if (objectOpenAdditional)
                {
                    additionalAnimator.Play("Close", 0, 0.0f);
                    objectOpenAdditional = false;
                    animateAdditional.GetComponent<SimpleOpenClose>().objectOpen = false;
                }
            }
            else
            {
                myAnimator.Play("Open", 0, 0.0f);
                objectOpen = true;
                PlaySound(openSound);
                animateAdditional.GetComponent<SimpleOpenClose>().objectOpenAdditional = true;

                if (!objectOpenAdditional)
                {
                    additionalAnimator.Play("Open", 0, 0.0f);
                    objectOpenAdditional = true;
                    animateAdditional.GetComponent<SimpleOpenClose>().objectOpen = true;
                }
            }
        }

        void PlaySound(AudioClip clip)
        {
            if (audioSource != null && clip != null)
            {
                audioSource.PlayOneShot(clip);
            }
        }

        bool CheckIfPlayerIsLooking()
        {
            Vector3 directionToDoor = (transform.position - playerCamera.position).normalized;
            float dotProduct = Vector3.Dot(playerCamera.forward, directionToDoor);
            return dotProduct > 0.5f;
        }

        IEnumerator CheckForAutoOpen()
        {
            while (true)
            {
                yield return new WaitForSeconds(3f);
                if (!CheckIfPlayerIsLooking() && !objectOpen && !isLocked)
                {
                    OnDoorShouldOpen?.Invoke(this);
                }
            }
        }

        public void ForceOpen()
        {
            if (!objectOpen && !isLocked)
            {
                myAnimator.Play("Open", 0, 0.0f);
                objectOpen = true;
                PlaySound(openSound);
            }
        }
    }
}