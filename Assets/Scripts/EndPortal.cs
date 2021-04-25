using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndPortal : MonoBehaviour
{
    [SerializeField] BoxCollider2D portalCollider;
    [SerializeField] SpriteRenderer sprite;

    bool p1Detected = false;
    bool p2Detected = false;

    // Update is called once per frame
    void Update()
    {
        if (!portalCollider.enabled && GameObject.Find("Enemies").transform.childCount == 0)
        {
            portalCollider.enabled = true;
            sprite.enabled = true;
        }

        if (p1Detected && p2Detected) NextLevel();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player1") p1Detected = true;
        if (collision.tag == "Player2") p2Detected = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player1") p1Detected = false;
        if (collision.tag == "Player2") p2Detected = false;

    }

    void NextLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }
}
