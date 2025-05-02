using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FS_Atmo;

public class DoorManager : MonoBehaviour
{
    public List<SimpleOpenClose> allDoors = new List<SimpleOpenClose>();
    public float checkInterval = 10f; // Time between spooky door events
    public float triggerDistance = 10f; // Max distance from player to trigger a door
    private Transform player;

    void Start()
    {
        player = Camera.main.transform;
        StartCoroutine(SpookyDoorRoutine());
    }

    IEnumerator SpookyDoorRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(checkInterval);

            List<SimpleOpenClose> nearbyDoors = new List<SimpleOpenClose>();

            // Find all unlocked doors near the player
            foreach (SimpleOpenClose door in allDoors)
            {
                if (door != null && !door.objectOpen && !door.isLocked) // Check if NOT locked
                {
                    float distance = Vector3.Distance(player.position, door.transform.position);
                    if (distance <= triggerDistance)
                    {
                        nearbyDoors.Add(door);
                    }
                }
            }

            // If there are valid doors, randomly open one
            if (nearbyDoors.Count > 0)
            {
                int randomIndex = Random.Range(0, nearbyDoors.Count);
                nearbyDoors[randomIndex].ForceOpen();
            }
        }
    }
}