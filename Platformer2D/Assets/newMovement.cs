using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newMovement : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    public int jump;
    public bool isGrounded;
    public int Direction = 1;
    public Vector2 boxSize;
    public float castDistance;
    public LayerMask groundLayer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
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

        if (Input.GetButtonDown("Jump") && isGrounded)       //getbuttondown-w momencie wciœniêcia klawisza tylko getbutton-ci¹gle gdy tylko trzymamy klawisz
        {
            Jump();
        }


    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position + Vector3.down * castDistance, boxSize);
    }

    void FixedUpdate()
    {
        float move = speed * Input.GetAxis("Horizontal");         //GetAxis zwraca nam float w zakresie od -1 do 1    //Horizontal to nasz domyœlny input z unity

        if (move > 0) { Direction = 1; }
        if (move < 0) { Direction = -1; }

        rb.velocity = new Vector3(move, rb.velocity.y, 0);          //rbvelocityy zachowuje domyœln¹ prêdkoœæ jaka by³a w unity, nie ma problemów z opadaniem postaci   //musi byæ zawarte na osiach x,y,z    //vector3 tworzy nam 3 mo¿liwœci ruchu

    }


    void Jump()
    {

        rb.velocity = new Vector3(rb.velocity.x, 0, 0);
        rb.AddForce(new Vector3(0, jump, 0));
    }
}

