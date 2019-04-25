using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour {

    public float x, y;
    public bool groundedp;

    public string controller;

    private GameObject player;
    private SpriteRenderer spriteRenderer;

    void Start () {

        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        player = GameObject.Find(controller);
        groundedp = player.GetComponent<PhysicsObject>().grounded;
        x = 15;
        y = 15;
        StartCoroutine(Despawn());

        if (!player.GetComponent<PlayerPlatformerController>().right)
        {
            x *= -1;
            spriteRenderer.flipX = true;
        }
            
        if (!groundedp)
        {
            y *= -1;
            spriteRenderer.flipY = true;
        }
            
    }
	
	void FixedUpdate ()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(x,y);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.name != player.name && other.GetComponent<PlayerPlatformerController>().guard == false)
        {
            if (player.GetComponent<PlayerPlatformerController>().right)
                other.GetComponent<Rigidbody2D>().velocity = new Vector2(200, 50);
            else
                other.GetComponent<Rigidbody2D>().velocity = new Vector2(-200, 50);

            other.GetComponent<Collider2D>().isTrigger = true;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;

        }
    }

    IEnumerator Despawn()
    {
        yield return new WaitForSeconds(1.5f);
        player.GetComponent<PlayerPlatformerController>().fireCount += 1;
        Destroy(gameObject);

    }

}
