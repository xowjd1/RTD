using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    public float damage;
    public float range;
    public float attackSpeed;
    
    protected float lastAttackTime;
    
    public virtual void Initialize(TowerData data)
    {
        damage = data.damage;
        range = data.range;
        attackSpeed = data.attackSpeed;
    }

    protected virtual void Update()
    {
        if (Time.time - lastAttackTime >= attackSpeed)
        {
            Attack();
            lastAttackTime = Time.time;
        }
    }
    public virtual void Upgrade()
    {
        damage *= 1.2f;
        attackSpeed *= 0.9f;
    }


    protected abstract void Attack();
}
