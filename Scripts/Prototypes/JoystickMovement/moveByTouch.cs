    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveByTouch : MonoBehaviour
{
    // Update is called once per frame
    /*Input.GetTouch(0) this replicates the first touch zero'th indexing

    Touch touch = Input.GetTouch(0); //touch variable will store our first touch

        touch pos will give the position of our current touch.
        touch phase will give the current state of our touch if its began,end ,moved,stationary,cancel etc.


    Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position); //will convert screen position to world position and store it in variable.
    touchPosition.z = 0f; //dont want to move the player in z-axis
    transform.position = touchPosition;

    for(int i=0;i<Input.touchCount; i++) 
    {
        Vector3 touchPosition = Camera.main.ScreenToWorldPoint(Input.touches[i].position);
        //this will take the array of touches like first touch or second touch and store it in touch position
        Debug.DrawLine(Vector3.zero, touchPosition,Color.red); //Draw's a line from Vector3.zero means from center of World to touchPos.
    }
    */
    public float moveSpeed = 5f;
    public Joystick joystick;
    Animator animator;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    float xInput, yInput;
    public swordAttack attack;
    public float playerHealth = 100; //stores the players health
    public bool playerIsDead = false;
    public float healthPoints = 25;
    public PlayerHealth incHealth;
    public HealthSpawner hS;
    public float pHealth
    {
        set 
        {
            playerHealth = value;
            //Debug.Log("INSIDE THE PLAYER SCRIPT the value is" + playerHealth);
            if(playerHealth <= 0)
            {
                //Debug.Log("DED INSIDE");
                StartCoroutine(PlayerDead());
            }
        }

        get 
        {
            if(!playerIsDead)
            {
                StartCoroutine(PlayerFlash());
            }
            return playerHealth; 
        }
    }
    
    void Start()
    {

        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        incHealth = GameObject.FindObjectOfType<PlayerHealth>();
        hS = GameObject.FindObjectOfType<HealthSpawner>();
    }

    void Update()
    {

        //float xInput = joystick.Horizontal * moveSpeed * Time.deltaTime;
        //float yInput = joystick.Vertical * moveSpeed * Time.deltaTime;
        if (!playerIsDead)
        {
            xInput = joystick.Horizontal * moveSpeed * Time.deltaTime;
            yInput = joystick.Vertical * moveSpeed * Time.deltaTime;
            rb.velocity = new Vector2(xInput, yInput);
            transform.Translate(xInput, yInput, 0);


            if (xInput > 0)
            {
                animator.SetBool("isMovingLR", true);
                spriteRenderer.flipX = false;
                //print(xInput);
                //Debug.Log("Moving Right");
            }
            else if (xInput < 0)
            {
                spriteRenderer.flipX = true;
                animator.SetBool("isMovingLR", true);
                //print(xInput);
                //Debug.Log("Moving Left");
            }
            else
            {
                animator.SetBool("isMovingLR", false);
                //Debug.Log("Is Idle");
            }

            if (yInput > 0)
            {

                animator.SetBool("isMovingUp", true);
                //print(yInput);
                //Debug.Log("Moving Up");
            }
            else if (yInput < 0)
            {
                animator.SetBool("isMovingDown", true);
                //print(yInput);
                //Debug.Log("Moving Down");
            }
            else
            {
                animator.SetBool("isMovingUp", false);
                animator.SetBool("isMovingDown", false);
                //Debug.Log("Is Idle");
            }
        }
    }

    //Player took the health
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Health"))
            {
                if (playerHealth < 100)
                { 
                    pHealth += healthPoints;//increments the player health;
                    incHealth.slider.value += healthPoints; //increments the health in slider
                }
                if (playerHealth > 100)
                {
                    incHealth.slider.value = 100;
                }
                Destroy(other.gameObject);
                //hS.isHealth = true;
                //Debug.Log("From Player Script Player health is" + pHealth);
            }
    }
    public void Attack()
    {    
        if (!playerIsDead)
        {
            animator.SetTrigger("isAttack");
            if (xInput > 0)  //will set the hitBox at the right side
            {
                attack.AttackRight();
            }
            else if (xInput < 0) //will set the hitBox at the left side
            {
                attack.AttackLeft();
            }
        }
    }

    IEnumerator PlayerDead()
    {
        playerIsDead = true;
        animator.SetTrigger("playerDead");
        yield return new WaitForSeconds(2.0f);
        Destroy(gameObject);
    }

    IEnumerator PlayerFlash()
    {
        spriteRenderer.color = Color.black;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
    }
}
