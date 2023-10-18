using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordAttack : MonoBehaviour
{
    // Start is called before the first frame update
    Vector2 rightAttackOffset;
    public Collider2D swordCollider;//works for any Collider Shapes
    public float damage = 3;

    void Start()
    {
        rightAttackOffset = transform.localPosition;
    }
    public void AttackRight()
    {
        //Debug.Log("Right Attack");
        swordCollider.enabled = true;
        transform.localPosition = rightAttackOffset;
    }

    public void AttackLeft() 
    {
        //Debug.Log("Attack Left");
        transform.localPosition = new Vector3(rightAttackOffset.x * -1, rightAttackOffset.y);
        swordCollider.enabled = true;
    }
   
    public void StopAttack()
    {
        //Debug.Log("Attack Stop");
        swordCollider.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if(enemy != null ) 
            {
                enemy.Health -= damage;
            }
        }
    }
}
