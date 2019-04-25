using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMehjeek : MonoBehaviour {

    private bool go;
    private bool back;
    private float speed;
    private float x = 6;
    private Transform playerPos;
    private GameObject player;
    public string controller;
    private bool right;
    private SpriteRenderer spriteRenderer;

    private bool flipSprite;


    void Start ()
    {

        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        player = GameObject.Find(controller);
        playerPos = player.GetComponent<Transform>();
        speed = 15;
        go = true;
        back = false;
        StartCoroutine(Arms());

        if (!player.GetComponent<PlayerPlatformerController>().right)
            x *= -1;

    }
	
	void FixedUpdate ()
    {

        if (go)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(x, 0);
 
            if (gameObject.GetComponent<Rigidbody2D>().velocity.x < 0)
            {

                right = false;
                spriteRenderer.flipX = true;
              

            }
            else
            {
                right = true;

            }

        }

        if (back)
        {

            transform.position = Vector2.MoveTowards(transform.position, playerPos.position, speed * Time.deltaTime);

            if (right)
            {
                spriteRenderer.flipX = true;
            }

            if (!right)
            {
                spriteRenderer.flipX = false;
            }

        }

          


        
    }

    IEnumerator Arms()
    {
        yield return new WaitForSeconds(0.6f);
        go = false;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        yield return new WaitForSeconds(0.4f);
        back = true;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.name != player.name && !other.GetComponent<PlayerPlatformerController>().guard)
        {

            other.GetComponent<Rigidbody2D>().velocity = new Vector2(35 * x, 50);
            other.GetComponent<Collider2D>().isTrigger = true;
        }

        if (other.name == player.name && back)
        {

            player.GetComponent<PlayerPlatformerController>().fireCount++;

            Destroy(gameObject);
        }
            
    }
}
