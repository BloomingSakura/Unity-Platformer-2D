using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed;
    Rigidbody2D rb;
    public int direction = -1;
    public LayerMask mask;
    SpriteRenderer sr;
    CharacterMOvement chr;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        if(playerObj != null )
        {
            chr = playerObj.GetComponent<CharacterMOvement>();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if(IsPlayerAbove(collision.transform))
            {
                Die();
            }
            else
            {
                chr.DeadPlayer();
            }
        }
    }
    private bool IsPlayerAbove(Transform player)
    {
        return player.position.y > transform.position.y + 0.2f;
    }
    // Update is called once per frame
    void Update()
    {
        RaycastHit2D wall = Physics2D.Raycast(transform.position, new Vector3(direction, 0, 0), 0.4f, mask);
        if(wall.collider != null && wall.collider.tag != "Player")
        {
            direction = - direction;
            if(sr.flipX == true)
            {
                sr.flipX = false;
            }
            else
            {
                sr.flipX = true;
            }         
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(direction * 0.4f, 0, 0));
    }
    private void FixedUpdate()
    {
        float move = speed * direction;
        rb.velocity = new Vector3(move, rb.velocity.y, 0);
    }
    private void Die()
    {
        animator.SetBool("isDead", true);
        Destroy(gameObject.GetComponent<BoxCollider2D>());
        Destroy(gameObject.GetComponent<EnemyMovement>());
        Destroy(gameObject.GetComponent<Rigidbody2D>());
    }
}
