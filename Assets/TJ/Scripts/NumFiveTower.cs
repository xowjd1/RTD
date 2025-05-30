using UnityEngine;

public class NumFiveTower : Tower
{
    public GameObject projectilePrefab;

    protected override void Attack()
    {
        Debug.Log("5번 타워 공격 시도");
        
        GameObject target = FindNearestEnemy();
        if (target != null)
        {
            Vector2 direction = (target.transform.position - transform.position).normalized;

            GameObject proj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            proj.GetComponent<PiercingProj>().Init(direction, damage);
            Debug.Log("▶ 투사체 생성됨", proj);
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

    public override void Initialize(TowerData data)
    {
        base.Initialize(data);
    }
}