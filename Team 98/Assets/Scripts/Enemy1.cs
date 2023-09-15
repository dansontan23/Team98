using System.Collections;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float moveSpeed = 20f;
    [SerializeField] float startFollowingDistance = 10f; // Distance at which the enemy starts moving towards the player.
    [SerializeField] float shootingDistance = 5f; // Distance at which the enemy starts shooting.
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float shootingInterval = 2f;
    [SerializeField] private int meleeDamageAmount = 10;
    
    private void Start()
    {
        StartCoroutine(ShootingRoutine());
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        
        // Only move towards player if the distance is greater than startFollowingDistance
        // and less than shootingDistance.
        if (distanceToPlayer > shootingDistance && distanceToPlayer <= startFollowingDistance)
        {
            MoveTowardsPlayer();
        }
    }

    void MoveTowardsPlayer()
    {   
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
    }


    IEnumerator ShootingRoutine()
    {
        while (true)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            if (distanceToPlayer <= shootingDistance)
            {
                ShootBullet();
                Debug.Log("Player's position: " + player.position);

            }
            yield return new WaitForSeconds(shootingInterval);
        }
    }

    void ShootBullet()
    {
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        float rotationZ = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
        GameObject bulletInstance = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0.0f, 0.0f, rotationZ ));
        Bullet bulletScript = bulletInstance.GetComponent<Bullet>();
        bulletScript.SetDirection(directionToPlayer);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Damageable playerDamageable = collision.gameObject.GetComponent<Damageable>();
            if (playerDamageable != null)
            {
                playerDamageable.TakeDamage(meleeDamageAmount);
            }
        }
    }
}
