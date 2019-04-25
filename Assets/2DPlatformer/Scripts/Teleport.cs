using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour {

    public float distance;
    public bool lookDirection;

    private float localx;
    private float localy;
    private GameObject player;
    private int coolDown;
    private Collider2D tpDamageArea;
    public bool canDamage;

    private bool doIt;
    public bool tp;
    public string controller;

    void Start ()
    {
        player = GameObject.Find(controller);
        coolDown = 0;
        canDamage = false;
    }
	

	void Update ()
    {
        lookDirection = player.GetComponent<PlayerPlatformerController>().right;    
        localx = transform.localPosition.x;
        localy = transform.localPosition.y;


        if (tp && coolDown <= 0)
        {


            gameObject.GetComponent<CircleCollider2D>().enabled = true;
            canDamage = true;

            if (lookDirection && !doIt)
            {
                StartCoroutine(Delay());

            }
                

            else if (!lookDirection && !doIt)
            {
                StartCoroutine(DelayLeft());
            }
                
            

           
            
        }
            
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (canDamage && other.name != player.name && !other.GetComponent<PlayerPlatformerController>().guard)
        {

            other.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 160);
            other.GetComponent<Collider2D>().isTrigger = true;

        }
    }

    IEnumerator CoolDown()
    {
        coolDown = 1;
        yield return new WaitForSeconds(0.2f);
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        canDamage = false;
        yield return new WaitForSeconds(1.8f);
        coolDown = 0;
        tp = false;
        doIt = false;
    }

    IEnumerator Delay()
    {
        gameObject.GetComponent<Animator>().SetTrigger("Teleport");

        doIt = true;
        yield return new WaitForSeconds(0.5f);
        transform.localPosition = new Vector2(distance + localx, distance + localy);
        StartCoroutine(CoolDown());
    }

    IEnumerator DelayLeft()
    {
        gameObject.GetComponent<Animator>().SetTrigger("Teleport");


        doIt = true;
        yield return new WaitForSeconds(0.5f);
        transform.localPosition = new Vector2(localx - distance, localy + distance);
        StartCoroutine(CoolDown());
    }

}
