using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FS_Atmo
{
    public class SimplePlayerUse : MonoBehaviour
    {
        public GameObject mainCamera;
        private GameObject objectClicked;
        public KeyCode OpenClose;

        void Start()
        {

        }

        void Update()
        {
            if (Input.GetKeyDown(OpenClose)) // Open and close action
            {
                RaycastCheck();
            }
        }

        void RaycastCheck()
        {
            RaycastHit hit;

            if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.TransformDirection(Vector3.forward), out hit, 5f)) // Extended range
            {
                if (hit.collider.gameObject.GetComponent<SimpleOpenClose>())
                {
                    // Debug.Log("Object with SimpleOpenClose script found");
                    hit.collider.gameObject.BroadcastMessage("ObjectClicked");
                }
                else
                {
                    // Debug.Log("Object doesn't have script SimpleOpenClose attached");
                }
                // Debug.DrawRay(mainCamera.transform.position, mainCamera.transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                // Debug.Log("Did Hit");
            }
            else
            {
                // Debug.DrawRay(mainCamera.transform.position, mainCamera.transform.TransformDirection(Vector3.forward) * 1000, Color.white);
                // Debug.Log("Did not Hit");
            }
        }
    }
}
