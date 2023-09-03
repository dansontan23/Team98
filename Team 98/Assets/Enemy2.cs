using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy2 : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float damageAmount = 0.5f;
    [SerializeField] Transform player; // Reference to the player's transform for following.

    private float health = 1f;

    private void Start()
    {
        if (!player)
        {
            player = FindObjectOfType<Player>().transform; // Assuming you have a Player script attached to your player character.
        }
    }

    private void Update()
    {
        FollowPlayer();
    }

    void FollowPlayer()
    {
        if (!player) return;

        // Calculate direction towards the player.
        Vector2 direction = (player.position - transform.position).normalized;
        
        // Move towards the player.
        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Assuming you have a Player script attached to your player character with a method to take damage.
        if (collision.gameObject.CompareTag("Player")) 
        {
            collision.gameObject.GetComponent<Player>().TakeDamage(damageAmount);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy(gameObject); // Destroy this enemy if health goes to zero or below.
        }
    }
}
