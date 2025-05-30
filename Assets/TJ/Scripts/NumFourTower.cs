using UnityEngine;

public class NumFourTower : Tower
{
    public GameObject projectilePrefab;

    protected override void Attack()
    {
        GameObject target = FindNearestEnemy();
        if (target != null)
        {
            ShootSpread(target);
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

    private void ShootSpread(GameObject target)
    {
        Vector2 direction = (target.transform.position - transform.position).normalized;

        // 중심 각도 계산
        float baseAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // 3발 발사: -20°, 0°, +20°
        for (int i = -1; i <= 1; i++)
        {
            float angleOffset = 20f * i;
            float angle = baseAngle + angleOffset;
            Vector2 shootDir = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));

            GameObject proj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            proj.GetComponent<Projectile>().Init(shootDir.normalized, damage); // 방향 기반 초기화
        }
    }

    public override void Initialize(TowerData data)
    {
        base.Initialize(data);
        // projectilePrefab = data.projectilePrefab; // 필요하면
    }
}
