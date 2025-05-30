using System.Collections;
using UnityEngine;

public class TowerGrid : MonoBehaviour
{
    public TowerDataBase towerDB;
    public bool hasTower = false;

    public void SetTower() => hasTower = true;
    public void ClearTower() => hasTower = false;

    public void TrySpawnTower()
    {
        if (hasTower) return;

        // 💰 자원 체크 및 차감 시도
        if (!GameManager.Instance.TryBuyTower())
        {
            Debug.Log("자원이 부족하여 타워를 배치할 수 없습니다.");
            return;
        }

        // 자원 충분 → 타워 생성
        TowerData randomTower = towerDB.GetRandomTower();
        if (randomTower != null)
        {
            GameObject newTower = Instantiate(randomTower.prefab, transform.position, Quaternion.identity);
            StartCoroutine(ResetColliderNextFrame(newTower));

            TowerObject towerObj = newTower.GetComponent<TowerObject>();
            towerObj.Init(randomTower, this);

            // 🔥 추가: 현재 업그레이드 레벨만큼 Upgrade()
            Tower towerComponent = newTower.GetComponent<Tower>();
            if (towerComponent != null)
            {
                int upgradeLevel = GameManager.Instance.GetUpgradeLevel(randomTower.raceType);
                for (int i = 1; i < upgradeLevel; i++)
                {
                    towerComponent.Upgrade();
                }
            }

            Physics2D.SyncTransforms(); // 강제 물리 갱신
        }
    }

    
    private IEnumerator ResetColliderNextFrame(GameObject target)
    {
        yield return null;
    
        Collider2D col = target.GetComponent<Collider2D>();
        if (col != null)
        {
            col.enabled = false;
            yield return null;
            col.enabled = true;
        }
    }
}
