using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TowerDatabase", menuName = "Tower/New TowerDatabase")]
public class TowerDataBase : ScriptableObject
{
    public List<TowerData> allTowers;

    public TowerData GetRandomTower()
    {
        if (allTowers == null || allTowers.Count == 0) return null;

        // 랜덤 종족 (균등 분포)
        RaceType randomRace = (RaceType)Random.Range(0, System.Enum.GetValues(typeof(RaceType)).Length);

        // 확률 기반 레벨
        int randomLevel = GetRandomLevel();

        // 해당 조건에 맞는 타워 리스트
        List<TowerData> candidates = allTowers.FindAll(t => t.raceType == randomRace && t.level == randomLevel);
        if (candidates.Count == 0) return null;

        return candidates[Random.Range(0, candidates.Count)];
    }

// 확률 기반 레벨 선택
    private int GetRandomLevel()
    {
        float rand = Random.value * 100f;

        if (rand < 52f) return 1;
        else if (rand < 77f) return 2;
        else if (rand < 87f) return 3;
        else if (rand < 94f) return 4;
        else if (rand < 98.9f) return 5;
        else if (rand < 99.9f) return 6;
        else return 7;
    }

}