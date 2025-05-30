using UnityEngine;

public class PiercingProj : MonoBehaviour
{
    private Vector2 moveDirection;
    private float speed = 13f;
    private float damage;
    private float lifetime = 3f; // 3초 후 파괴

    public void Init(Vector2 direction, float dmg)
    {
        moveDirection = direction.normalized;
        damage = dmg;

        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        transform.position += (Vector3)(moveDirection * speed * Time.deltaTime);
        Debug.Log("💨 투사체 이동 중");

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if (collision.TryGetComponent<Enemy>(out var enemy))
            {
                enemy.TakeDamage((int)damage);
            }
        }
    }
}
