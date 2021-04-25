using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] float aggroRange;
    [SerializeField] float moveSpeed;
    [SerializeField] LayerMask Ground;
    [SerializeField] LayerMask PlayerCast;

    Transform player;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.right * moveSpeed * Time.deltaTime * 60f;

        //Get correct target player
        switch (tag)
        {
            case "RedEnemy":
                player = GameObject.FindGameObjectWithTag("Player1").transform;
                break;
            case "GreenEnemy":
                player = GameObject.FindGameObjectWithTag("Player2").transform;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (CanSeePlayer())
        {
            if (transform.position.x < player.position.x) transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
            else transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
            rb.velocity = Vector3.right * moveSpeed * transform.localScale.x;
        }
        else if (Mathf.Abs(rb.velocity.x) < 0.2f) //Swap direction after hitting an obstacle
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            rb.velocity = Vector3.right * moveSpeed * transform.localScale.x;
        }
    }

    bool CanSeePlayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, player.position - transform.position, aggroRange, PlayerCast);
        if (hit && hit.collider.tag == player.tag) return true;
        return false;
    }
}