using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAttack : MonoBehaviour
{
    public Collider2D enemyCollider;
    public Collider2D enemyCapsuleCollider;
    Vector2 enemyAttackOffset;
    public float pdamage = 3f;
    public float p;
    public PlayerHealth playershealth;

    void Start()
    {
        enemyAttackOffset = transform.localPosition;
        playershealth = GameObject.FindObjectOfType<PlayerHealth>();
        p = 100f;
    }

    public void EnemyAttackRight()
    {
        enemyCollider.enabled = true;
        transform.localPosition = enemyAttackOffset;
    }
    public void EnemyAttackLeft()
    {
        enemyCapsuleCollider.enabled = true;
        transform.localPosition = enemyAttackOffset;
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if(c.tag == "Player")
        {
            moveByTouch player = c.GetComponent<moveByTouch>();
            if(player != null ) 
            {
                player.pHealth -= pdamage;
                p = player.pHealth;
                playershealth.UpdatePlayerHealth(p);
                //Debug.Log("From Enemy Attack PLAYER HEALTH IS " + p);
            }
        }
    }
}
