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

        TowerData randomTower = towerDB.GetRandomTower();
        if (randomTower != null)
        {
            GameObject newTower = Instantiate(randomTower.prefab, transform.position, Quaternion.identity);
            StartCoroutine(ResetColliderNextFrame(newTower));
            TowerObject towerObj = newTower.GetComponent<TowerObject>();
            towerObj.Init(randomTower, this);

            // 강제로 Physics 갱신
            Physics2D.SyncTransforms();
        }
    }
    
    private IEnumerator ResetColliderNextFrame(GameObject target)
    {
        yield return null;
    
        Collider2D col = target.GetComponent<Collider2D>();
        if (col != null)
        {
            col.enabled = false;
            yield return null; // 한 프레임 더 기다리면 확실함
            col.enabled = true;
        }
    }
}
