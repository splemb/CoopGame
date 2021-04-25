using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPressurePlate : MonoBehaviour
{
    [SerializeField] IDoor door;
    [SerializeField] float timerMax;
    float timer;

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                timer = 0;
                door.CloseDoor();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player1" || collider.tag == "Player2")
        {
            //Player entered collider
            door.OpenDoor();
            timer = timerMax;
        }
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.tag == "Player1" || collider.tag == "Player2")
        {
            //Player still on top of collider
            timer = timerMax;
        }
    }
}