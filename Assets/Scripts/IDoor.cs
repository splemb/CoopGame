using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IDoor : MonoBehaviour
{
    [SerializeField] BoxCollider2D doorCollider;
    [SerializeField] SpriteRenderer sprite;

    public void OpenDoor()
    {
        sprite.enabled = false;
        doorCollider.enabled = false;
    }

    public void CloseDoor()
    {
        sprite.enabled = true;
        doorCollider.enabled = true;
    }

}