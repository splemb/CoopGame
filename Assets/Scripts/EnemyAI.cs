using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    Transform player;
    [SerializeField]
    float agroRange;
    [SerializeField]
    float moveSpeed;
    [SerializeField]
    Transform castPoint;
    public float distence;
    private bool movingRight = true;
    bool movingLeft = false;
    bool isAgro = false;
    float timer = 1f;
    public Transform groundDetection;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        // distence to player
        float distToPlayer = Vector2.Distance(transform.position, player.position);
        //Debug.Log("Distence to player: " + distToPlayer); 

        float castDist = distence;
        if (movingLeft == true)
        {
            castDist = -distence;
        }
        if (CanSeePlayer(agroRange))
        {
            isAgro = true;
        }
        else if (isAgro == false)
        {
            StopChasing();
        }
        if (isAgro)
        {
            ChasePlayer();
        }

    }

    void ChasePlayer()
    {
        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, 1);
        if (groundInfo.collider == true) // stop enemy going over edge if player jumps across ledge
        {
            if (transform.position.x < player.position.x) // enemy is on left of player so move right
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingLeft = false;
            }
            else // enemy is right side of the player move left
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingLeft = true;
            }
        }
        else
        {
            isAgro = false;
        }
    }

    void StopChasing()
    {
        isAgro = false;
        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime); // move right

        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, 1); // shoot raycast down to detect ground
        if (groundInfo.collider == false) // if the cast does not detect ground turn in opposite direction
        {
            if (movingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
                movingLeft = true;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
                movingLeft = false;
            }
        }
    }

    bool CanSeePlayer(float distence)
    {
        bool canSee = false;
        float castDist = distence;
        if (movingLeft == true)
        {
            castDist = -distence;
        }

        Vector2 endPos = castPoint.position + Vector3.right * castDist;
        RaycastHit2D hitInfo = Physics2D.Linecast(castPoint.position, endPos, 1 << LayerMask.NameToLayer("Player"));

        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.gameObject.CompareTag("Player1"))
            {
                canSee = true;
            }
            else
            {
                canSee = false;
            }
            Debug.DrawLine(castPoint.position, hitInfo.point, Color.red);
        }
        else
        {
            Debug.DrawLine(castPoint.position, endPos, Color.blue);
        }
        return canSee;
    }
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Debug.Log(hitInfo.name);
        if (hitInfo.tag == "Spikes") // if the hit object is an object which can be destoryed by spell destory it
        {
            isAgro = false;
            if (movingRight == true)
            {

                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
                movingLeft = true;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
                movingLeft = false;
            }
        }
        else if (hitInfo.tag == "Nails") // if the hit object is an object which can be destoryed by spell destory it
        {
            isAgro = false;
            if (movingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
                movingLeft = true;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
                movingLeft = false;
            }
        }
    }
}