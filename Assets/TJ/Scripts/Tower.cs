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

    protected abstract void Attack();
}
