using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] private int damage = 10;
    private Vector3 moveDirection;


    // Update is called once per frame
    void Update()
    {
        transform.position += moveDirection * speed * Time.deltaTime;
    }

    public void SetDirection(Vector3 direction)
    {
        moveDirection = direction.normalized;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Ensure that bullet only damages player
        if(other.gameObject.CompareTag("Player"))
        {
            Damageable target = other.GetComponent<Damageable>();
            if(target)
            {
                target.TakeDamage(damage);
                Destroy(gameObject); // Destroy bullet after hit
            }
        }
    }



}
