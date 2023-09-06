using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    // Damage value on sword; can be adjusted
    // public float damage = 3;

    Vector2 rightAttackOffset;
    public Collider2D swordCollider;

    // Start is called before the first frame update
    private void Start()
    {
        rightAttackOffset = transform.position;
    }

    // Enable attack right hitbox
    public void AttackRight()
    {
        // print("Attack Right");
        swordCollider.enabled = true;
        transform.localPosition = rightAttackOffset;
    }
    
    // Enable attack left hitbox
    public void AttackLeft()
    {
        // print("Attack Left");
        swordCollider.enabled = true;
        transform.localPosition = new Vector3(rightAttackOffset.x * -1, rightAttackOffset.y);
    }

    // End of the attack, disable the hitbox
    public void StopAttack()
    {
        swordCollider.enabled = false;
    }

/*  // Function that triggers damage upon sword attack on enemy; **requires enemy to have "Enemy" tag
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Enemy") {
            // Deal damage to the enemy
            Enemy enemy = other.GetComponent<Enemy>();

            if(enemy != null) {
                enemy.Health -= damage;
            }
        }
    } */
}
