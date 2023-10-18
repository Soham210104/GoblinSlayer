using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
{
    public float speed;
    private Transform target;
    Animator animator;
    public enemyAttack eAttack;
    SpriteRenderer enemySprite;
    bool isDead = false;
    //public Collider2D tr;//gets the component for trigger
    public moveByTouch playerScript;
    public float obstacleAvoidanceRadius = 1.0f;
    public float health = 10; //health of enemy
    //public static float enemyKillCount = 0;
    //public float storedCount = 0;
    public TextMeshProUGUI killText;
    public float Health
    {
        set
        {
            health = value;
            if (health <= 0)
            {
                StartCoroutine(DefeatedCoroutine());
            }
        }
        get
        {
            StartCoroutine(FlashRed());
            return health;
        }
    }
    public float fixedDistance = 1.5f; // Define the fixed distance here

    void Start()
    {
        
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        animator = GetComponent<Animator>();
        enemySprite = GetComponent<SpriteRenderer>();
        playerScript = GameObject.FindObjectOfType<moveByTouch>();
        killText = GameObject.FindObjectOfType<TextMeshProUGUI>();
        //enemyKillCount = 0;
    }


    void Update()
    {
        if (playerScript != null && playerScript.playerIsDead) 
        {
            playerScript = null;
        }
        if (playerScript !=null &&!isDead && playerScript.playerIsDead == false) //run this script only when the enemy and player is not dead
        {
            float distanceToPlayer = Vector2.Distance(transform.position, target.position);

            //It checks for colliders that intersect with a specified circular area.In this case, it's checking around the transform.position of the current object.
            Collider2D[] nearbyEnemies = Physics2D.OverlapCircleAll(transform.position, obstacleAvoidanceRadius);

            for (int i = 0; i < nearbyEnemies.Length; i++)
            {
                Collider2D enemyCollider = nearbyEnemies[i];
                if (enemyCollider.gameObject != gameObject)
                {
                    Vector2 avoidDirection = (transform.position - enemyCollider.transform.position).normalized;
                    transform.position = (Vector2)transform.position + avoidDirection * speed * Time.deltaTime;
                }
            }

            if (distanceToPlayer > fixedDistance)
            {
                //If the player is far away then enemy should move towards the player
               
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

                if (target.position.x > transform.position.x)
                {
                    //if the player is at the right side of the enemy as its x coordinate is more than of enemy
                    animator.SetBool("WalkLeft", false);
                    animator.SetBool("WalkRight", true);
                    
                }
                else if (target.position.x < transform.position.x)
                {
                   //if the player is at the left side of the enemy as its x coordinate is less than of enemy
                    animator.SetBool("WalkRight", false);
                    animator.SetBool("WalkLeft", true);
                }

                // Reset attack animations
                animator.SetBool("RightAttack", false);
                animator.SetBool("LeftAttack", false);
            }
            else
            {
               //this is the case when distance is less than fixdistance means enemy and player are close
                
                animator.SetBool("WalkLeft", false);
                animator.SetBool("WalkRight", false);

                if (target.position.x > transform.position.x)
                {
                    //when the player is at the right side of enemy,enemy right attack animation is played
                    
                    eAttack.EnemyAttackRight();
                    animator.SetBool("RightAttack", true);   
                }
                else if (target.position.x < transform.position.x)
                {
                    // when the player is at the left side of enemy,enemy left attack animation is played
                    
                    eAttack.EnemyAttackLeft();
                    animator.SetBool("LeftAttack", true);
                }
            }
            
            if(playerScript.playerIsDead == true)
            {
                //Debug.Log("PLAYER IS DEADDDDDDDDDDDDD");
            }
        }
    }


    // Coroutine for death animation
    IEnumerator DefeatedCoroutine()
    {
        isDead = true;
        animator.SetTrigger("isDead");
        yield return new WaitForSeconds(2.0f);//animation of isDead is played and then it waits for 2seconds then the object is destroyed
        Destroy(gameObject);
        KillCounter.instance.IncreaseEnemyKillCount();//This will increase the enemy kill count through singleton reference
    }

    IEnumerator FlashRed()
    {
        enemySprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        enemySprite.color = Color.white;
    }
}
