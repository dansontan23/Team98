using System.Collections;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    [SerializeField] float moveSpeed = 20f;
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float shootInterval = 5f;
    [SerializeField] float laserDuration = 1f;

    [SerializeField] float damageAmount = 0.5f; // Damage amount.
    private float health = 1f; // Health attribute.

    private bool moveHorizontal = false;
    private bool moveVertical = false;
    private bool isShooting = false;

    private void Start()
    {
        DetermineMovementDirection();
        StartCoroutine(ShootingRoutine());
    }

    private void Update()
    {
        if (isShooting) return;

        if (moveHorizontal)
        {
            float LRAmount = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
            transform.Translate(LRAmount, 0, 0);
        }
        else if (moveVertical)
        {
            float UDAmount = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
            transform.Translate(0, UDAmount, 0);
        }
    }

    void DetermineMovementDirection()
    {
        if (Mathf.Abs(transform.position.x) > Mathf.Abs(transform.position.y))
        {
            moveVertical = true;
        }
        else
        {
            moveHorizontal = true;
        }
    }

    IEnumerator ShootingRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(shootInterval);

            isShooting = true;
            ShootLaser();
            yield return new WaitForSeconds(laserDuration);
            isShooting = false;
        }
    }

    void ShootLaser()
    {
        Vector3 direction = moveHorizontal ? Vector3.right : Vector3.up;
        GameObject laser = Instantiate(laserPrefab, transform.position + direction, Quaternion.identity);
        // Destroy or deactivate the laser after a duration.
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
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
            Destroy(gameObject); // Destroy this enemy if health is zero or below.
        }
    }
}
