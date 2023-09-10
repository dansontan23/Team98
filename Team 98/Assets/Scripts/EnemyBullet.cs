using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed = 10f;
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

}
