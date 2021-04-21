using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPresurePlate : MonoBehaviour
{
    [SerializeField] private Door door;

    private float timer;

    private void Update()
    {
        if (timer > 0f)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                door.CloseDoor();
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        float timeToStayOpen = 2f;
        timer = timeToStayOpen;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        door.OpenDoor();
    }

}
