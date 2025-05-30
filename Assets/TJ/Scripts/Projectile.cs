using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector2 moveDirection;
    private float speed = 8f;
    private float damage;
    private Transform target;
    private bool isDirectionMode = false;

    public void Init(Transform target, float dmg)
    {
        this.target = target;
        damage = dmg;
        isDirectionMode = false;
    }

    public void Init(Vector2 direction, float dmg)
    {
        moveDirection = direction.normalized;
        damage = dmg;
        isDirectionMode = true;
    }
    private void Start()
    {
        Destroy(gameObject, 3f);
    }
    private void Update()
    {
        if (isDirectionMode)
        {
            transform.position += (Vector3)(moveDirection * speed * Time.deltaTime);
        }
        else
        {
            if (target == null)
            {
                Destroy(gameObject);
                return;
            }

            Vector3 dir = (target.position - transform.position).normalized;
            transform.position += dir * speed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if (collision.TryGetComponent<Enemy>(out var enemy))
            {
                enemy.TakeDamage((int)damage);
            }

            Destroy(gameObject);
        }
    }
}