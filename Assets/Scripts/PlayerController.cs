using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    [SerializeField] public LayerMask Ground;

    private Rigidbody2D rb;
    private SpriteRenderer sprite;

    public bool willJump;
    public float canJump;

    public float moveAxis = 0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Only register jump if on the ground
        checkCanJump();

        if (tag == "Player1")
        {
            if ((canJump > 0) && Input.GetKeyDown(KeyCode.W))
            {
                willJump = true;
                canJump = 0;
            }
        }
        else if (tag == "Player2")
        {
            if ((canJump > 0) && Input.GetKeyDown(KeyCode.UpArrow))
            {
                willJump = true;
                canJump = 0;
            }
        }
    }

    private void FixedUpdate()
    {
        moveAxis = 0f;

        if (tag == "Player1")
        {
            if (Input.GetKey(KeyCode.D))
            {
                //transform.localScale = new Vector3(1,transform.localScale.y, transform.localScale.z);
                sprite.flipX = false;
                moveAxis = 1f;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                //transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
                sprite.flipX = true;
                moveAxis = -1f;
            }
        }
        else if (tag == "Player2")
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                //transform.localScale = new Vector3(1,transform.localScale.y, transform.localScale.z);
                sprite.flipX = false;
                moveAxis = 1f;
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                //transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
                sprite.flipX = true;
                moveAxis = -1f;
            }
        }

        if (willJump)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            willJump = false;
        }

        if (rb.velocity.y > 0.2 && ((tag == "Player1" && !Input.GetKey(KeyCode.W)) || (tag == "Player2" && !Input.GetKey(KeyCode.UpArrow))))
        {
            rb.velocity += new Vector2(0f, -rb.velocity.y);
        }

        rb.AddForce(new Vector2((moveAxis * speed) - rb.velocity.x, 0), ForceMode2D.Impulse);
    }

    private void checkCanJump()
    {
        if (isGrounded())
        {
            canJump = 0.05f;

        }
        else if (canJump > 0)
        {
            canJump -= Time.deltaTime;
        }
        else
        {
            canJump = 0;
        }
    }

    private bool isGrounded()
    {
        // Cast a box straight down
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, new Vector2(4f, 0.1f), 0f, -Vector2.up, 2f, Ground);

        // Return if it hits the ground
        return (hit.collider != null);
    }
}
