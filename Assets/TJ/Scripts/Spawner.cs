using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("오브젝트 풀러")]
    [SerializeField] private ObjectPooler[] poolers;

    [SerializeField] private WayPoint wayPoint;
    [SerializeField] private float spawnInterval = 1f;

    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnEnemyByStage(StageManager.Instance.CurrentStage);
            timer = 0f;
        }
    }

    private void SpawnEnemyByStage(int stage)
    {
        ObjectPooler selectedPooler = GetPoolerForStage(stage);
        if (selectedPooler == null)
        {
            Debug.LogWarning($"스테이지 {stage}에 해당하는 오브젝트 풀러가 없습니다.");
            return;
        }

        selectedPooler.ExpandPool(30); // 매 스테이지마다 30개 추가

        GameObject enemy = selectedPooler.GetInstanceFromPool();
        if (enemy == null)
        {
            Debug.LogWarning($"[풀 비어있음] {selectedPooler.name} 풀에 사용 가능한 오브젝트가 없습니다.");
            return;
        }

        enemy.transform.position = transform.position;
        enemy.SetActive(true);

        Enemy enemyScript = enemy.GetComponent<Enemy>();
        if (enemyScript == null)
        {
            Debug.LogError("Enemy 컴포넌트가 프리팹에 없습니다.");
            return;
        }

        enemyScript.Initialize(wayPoint);
    }

    private ObjectPooler GetPoolerForStage(int stage)
    {
        int poolIndex = (stage - 1) / 7; // 1~7 = 0, 8~14 = 1, ..., 43~49 = 6
        if (poolIndex >= 0 && poolIndex < poolers.Length)
        {
            return poolers[poolIndex];
        }
        return null;
    }
}