using System.Collections.Generic;
using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    public List<TowerData> allTowerData;

    public void SpawnTower(Vector3 position, TowerData data)
    {
        GameObject towerObj = Instantiate(data.prefab, position, Quaternion.identity);
        Tower tower = towerObj.GetComponent<Tower>();
        tower.Initialize(data);
    }
}
