using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformerController : PhysicsObject
{

    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 7;
    public string inputHorizontal;
    public string inputJump;
    public string inputAttack;
    public bool right;
    public int fireCount;
    public bool sy;
    public Vector2 move;
    public int lives;
    public int dashSpeed;
    public GameObject player;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private float jumpCount;
    private bool dash = false;
    private bool canDash = true;
    private bool canGuard = true;

    public bool guard = false;
    public GameObject projectile;
    public Transform firePointRight;
    public Transform firePointLeft;

    public KeyCode buttonDefense;
    public KeyCode buttonDash;
    public KeyCode buttonTP;

    void Awake()
    {

        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        jumpCount = 1;
        lives = 10;

        if (sy == true)
            fireCount = 1;
        else
            fireCount = 2;

    }


    protected override void ComputeVelocity()
    {
        if(fireCount > 2)
        {
            fireCount = 2;
        }

        else if(move.x == 1)
        {
            right = true;
            dashSpeed = 1100;
        }
        else if (move.x == -1)
        {
            right = false;
            dashSpeed = -1100;
        }

        if (Input.GetKeyDown(buttonDefense) && !guard && canGuard)
        {
            gameObject.transform.GetChild(2).gameObject.SetActive(true);
            StartCoroutine(Guard());
        }

         move = Vector2.zero;

        if(Input.GetKeyDown(buttonTP) && !sy)
        {
            this.GetComponent<Teleport>().tp = true;

        }


        if (Input.GetKeyDown(buttonDash) && sy && canDash)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
            animator.SetLayerWeight(2, 1);
            animator.SetTrigger("Dash");
            gameObject.GetComponent<Rigidbody2D>().AddForce(transform.right * dashSpeed);
            StartCoroutine(DashDamage());
            
        }
        else
        {
            move.x = Input.GetAxis(inputHorizontal);
        }

        if (Input.GetButtonDown(inputJump) && grounded)
        {
            //visco
            animator.SetTrigger("Jump");
            //

            velocity.y = jumpTakeOffSpeed;
        }
        else if (grounded)
        {
            jumpCount = 1;
        }
        else if (!grounded && jumpCount == 1 && Input.GetButtonDown(inputJump))
        {
            velocity.y = jumpTakeOffSpeed * 1.3f;
            jumpCount = 0;
            animator.SetTrigger("Jump");

        }

        //visco
        if (velocity.y < 0)
        {
            animator.ResetTrigger("Jump");
        }
        //
        
        if (Input.GetButtonDown(inputAttack) && fireCount >0 && right)
        {
            animator.SetTrigger("Attack");
            fireCount--;
            Instantiate(projectile, firePointRight.position, firePointRight.rotation);
            
        }
        if (Input.GetButtonDown(inputAttack) && fireCount > 0 && !right)
        {
            animator.SetTrigger("Attack");
            fireCount--;
            Instantiate(projectile, firePointLeft.position, firePointLeft.rotation);

        }

        bool flipSprite = (spriteRenderer.flipX ? (right) : (!right));
        if (flipSprite)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }        

        animator.SetBool("grounded", grounded);
        animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

        targetVelocity = move * maxSpeed;
        
        if (!grounded)
        {
            animator.SetLayerWeight(1, 1);
            animator.SetLayerWeight(0, 0);
        }
        else
        {
            animator.SetLayerWeight(0, 1);
            animator.SetLayerWeight(1, 0);
        }                

        if(velocity.y < 0)
        {
            
            animator.SetBool("Land", true);
        }
        if (grounded)
        {
            animator.SetBool("Land", false);
        }


     }

    public void StartGuard()
    {
        StartCoroutine(Guard());
    }


    public IEnumerator Guard()
    {
        guard = true;
        yield return new WaitForSeconds(1);
        gameObject.transform.GetChild(2).gameObject.SetActive(false);
        guard = false;
        StartCoroutine(GuardCooldown());
    }

    IEnumerator GuardCooldown()
    {
        canGuard = false;
        yield return new WaitForSeconds(2);
        canGuard = true;
    }

    IEnumerator DashDamage()
    {
        dash = true;
        canDash = false;
        yield return new WaitForSeconds(0.2f);
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        dash = false;
        yield return new WaitForSeconds(2f);
        canDash = true;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.name != this.name && other.GetComponent<PlayerPlatformerController>().guard == false && dash)
            if(right)
                other.GetComponent<Rigidbody2D>().velocity = new Vector2(200, 50);
        else
                other.GetComponent<Rigidbody2D>().velocity = new Vector2(-200, 50);

        other.GetComponent<Collider2D>().isTrigger = true;
    }

}