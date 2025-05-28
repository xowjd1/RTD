using UnityEngine;

public class TowerObject : MonoBehaviour
{
    public TowerData towerData;
    public TowerGrid owningGrid;
    private SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void Init(TowerData data, TowerGrid grid)
    {
        towerData = data;
        owningGrid = grid;
        grid.SetTower();
    }

    public void OnRelease(Vector3 fallbackPos)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 0.3f);
        foreach (var hit in hits)
        {
            if (hit != null && hit.gameObject != gameObject)
            {
                TowerObject other = hit.GetComponent<TowerObject>();
                if (other != null)
                {
                    TryMerge(other, fallbackPos);
                    return;
                }
            }
        }

        // 못 합치면 원래 자리로 복귀
        transform.position = fallbackPos;
    }

    public void SetSortingOrder(int order)
    {
        if (sr != null)
            sr.sortingOrder = order;
    }

    private void TryMerge(TowerObject other, Vector3 fallbackPos)
    {
        if (towerData.raceType != other.towerData.raceType || towerData.level != other.towerData.level)
        {
            Debug.Log("합성 조건 불충족");
            transform.position = fallbackPos;
            return;
        }

        int newLevel = towerData.level + 1;
        if (newLevel > 7)
        {
            Debug.Log("최대 레벨입니다.");
            transform.position = fallbackPos;
            return;
        }

        RaceType randomRace = (RaceType)Random.Range(0, System.Enum.GetValues(typeof(RaceType)).Length);
        TowerData newData = GameManager.Instance.towerDB.allTowers.Find(t => t.level == newLevel && t.raceType == randomRace);

        if (newData == null)
        {
            Debug.LogWarning($"❌ 못 찾음: {randomRace} Lv{newLevel}");
            transform.position = fallbackPos;
            return;
        }

        if (owningGrid != null) owningGrid.ClearTower();
        if (other.owningGrid != null) other.owningGrid.ClearTower();

        Destroy(other.gameObject);
        Destroy(gameObject);

        GameObject newTower = Instantiate(newData.prefab, other.transform.position, Quaternion.identity);
        TowerObject newObj = newTower.GetComponent<TowerObject>();
        newObj.Init(newData, other.owningGrid);
    }
}
