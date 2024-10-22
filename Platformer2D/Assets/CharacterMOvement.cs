using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterMOvement : MonoBehaviour
{
    private Rigidbody2D rb;
    public float moveSpeed;
    public float sprintSpeed;
    public bool isGrounded;
    public float jumpHeight = 5f;

    public Vector2 boxSize;
    public float castDistance;
    public LayerMask groundLayer;

    public float graviyScale = 2;
    public float fallGravityScale = 8;

    Animator animator;
    SpriteRenderer spr;
    // Start wywo³ujemy raz, na pocz¹tku uruchomienia skryptu
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
       spr = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        IsGrounded();

        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, 0.0f);
        float speed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : moveSpeed;
        transform.position += moveDirection * speed * Time.deltaTime;
 
        if(moveDirection.x != 0 )
        {
            animator.SetBool("isRunning", true);
            if (moveDirection.x < 0)
            {
                spr.flipX = true;
            }
            else
            {
                spr.flipX = false;
            }
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
        if(Input.GetKey(KeyCode.E))
        {
            animator.SetBool("isAttacking", true);
        }
        else
        {
            animator.SetBool("isAttacking", false);
        }
        bool isJumping = Input.GetKey(KeyCode.Space);

        if(isJumping && isGrounded)
        {   
            //dodajemy ForceMode2D
            rb.gravityScale = graviyScale;
            float jumpForce = Mathf.Sqrt(jumpHeight * (Physics2D.gravity.y * rb.gravityScale) * -2) * rb.mass;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        if(rb.velocity.y > 0)
        {
            rb.gravityScale = graviyScale;
        }
        else
        {
            rb.gravityScale = fallGravityScale;
        }
       
    }
 
    public void IsGrounded()
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, boxSize, 0, Vector3.down, castDistance, groundLayer);
        if (hit.collider != null)
        {
            isGrounded = true;
        }
        else
        {
           isGrounded = false;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position + Vector3.down * castDistance, boxSize);
    }

    public void DeadPlayer()
    {
        Destroy(gameObject);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
