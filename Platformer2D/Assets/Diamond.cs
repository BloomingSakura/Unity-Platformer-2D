using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{
    public float speed = 1f;
    public float amplitude = 0.3f;
    private Vector3 startPosition;
    CharacterMOvement chr;
    void Start()
    {
        startPosition = transform.position;
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        if (playerObj != null)
        {
            chr = playerObj.GetComponent<CharacterMOvement>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        float newPosition = startPosition.y + Mathf.Sin(Time.time * speed)* amplitude;
        transform.position = new Vector3(startPosition.x, newPosition, startPosition.z);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            chr.score++;
            Destroy(gameObject);
        }       
    }
}
