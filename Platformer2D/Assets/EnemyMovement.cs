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
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
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
}
