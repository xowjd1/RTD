using UnityEngine;

public class BasicTower : Tower
{
    public GameObject projectilePrefab;

    protected override void Attack()
    {
        GameObject target = FindNearestEnemy();
        if (target != null)
        {
            Shoot(target);
        }
    }

    private GameObject FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject nearest = null;
        float minDist = float.MaxValue;

        foreach (GameObject enemy in enemies)
        {
            float dist = Vector3.Distance(transform.position, enemy.transform.position);
            if (dist < minDist && dist <= range)
            {
                minDist = dist;
                nearest = enemy;
            }
        }

        return nearest;
    }

    private void Shoot(GameObject target)
    {
        GameObject proj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        proj.GetComponent<Projectile>().Init(target.transform, damage);
    }

    // 이걸로 TowerData와 projectilePrefab 모두 초기화
    public override void Initialize(TowerData data)
    {
        base.Initialize(data);

        // 혹시 TowerData에 projectilePrefab 필드가 있다면 여기에 설정 가능
        // projectilePrefab = data.projectilePrefab;
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}