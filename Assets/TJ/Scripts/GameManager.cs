using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public TowerDataBase towerDB;

    public int playerLife = 3;
    public int monsterCount = 0;
    
    public int gold = 10;
    private int towerBuyCount = 0;
    
    public TMP_Text enemyCountText;
    public TMP_Text goldText;
    public TMP_Text lifeText;
    public TMP_Text ninjaLevelText;
    public TMP_Text gunLevelText;
    public TMP_Text staffLevelText;
    
    public GameObject gameOverUI;

    private Dictionary<RaceType, int> raceUpgradeLevels = new Dictionary<RaceType, int>();
    
    private bool lifeDrainRunning = false;
    private void Awake()
    {
        Instance = this;
    }
    
    private void Start()
    {
        UpdateEnemyUI();
        UpdateGoldUI();
        UpdateLifeUI();
            
        raceUpgradeLevels[RaceType.Ninja] = 1;
        raceUpgradeLevels[RaceType.Gun] = 1;
        raceUpgradeLevels[RaceType.Staff] = 1;
    }
    
    public void ChangeMonsterCount(int delta)
    {
        monsterCount += delta;
        if (monsterCount < 0) monsterCount = 0;
        
        UpdateEnemyUI();

        if (monsterCount >= 100 && !lifeDrainRunning)
        {
            StartCoroutine(DrainLifeOverTime());
        }
    }
    private void UpdateEnemyUI()
    {
        if (enemyCountText != null)
            enemyCountText.text = $"{monsterCount} / 100";
    }

    private IEnumerator DrainLifeOverTime()
    {
        lifeDrainRunning = true;

        while (monsterCount >= 100 && playerLife > 0)
        {
            yield return new WaitForSeconds(5f);
            playerLife--;

            Debug.Log($"⛔ 생명 감소! 현재 생명: {playerLife}");
            UpdateLifeUI();

            if (playerLife <= 0)
            {
                Debug.Log("💀 게임 오버!");
                GameOver();
                yield break;
            }
        }

        lifeDrainRunning = false;
    }
    public bool TryBuyTower()
    {
        int cost = towerBuyCount + 1;

        if (gold >= cost)
        {
            gold -= cost;
            towerBuyCount++;
            UpdateGoldUI();
            return true;
        }

        Debug.Log("자원이 부족합니다!");
        return false;
    }

    public void RewardCurrencyForKill()
    {
        int reward = GetKillReward();
        gold += reward;
        UpdateGoldUI();
    }

    private int GetKillReward()
    {
        int stage = StageManager.Instance.CurrentStage;

        if (stage < 10) return 1;
        if (stage < 20) return 2;
        if (stage < 30) return 3;
        return 4;
    }

    private void UpdateGoldUI()
    {
        if (goldText != null)
            goldText.text = $"GOLD : {gold}";
    }

    private void UpdateLifeUI()
    {
        if (lifeText != null)
            lifeText.text = $"LIFE : {playerLife}";
    }


    private void GameOver()
    {
        Time.timeScale = 0f; 
        if (gameOverUI != null)
            gameOverUI.SetActive(true);
        else
            Debug.LogWarning("⚠️ GameOverUI가 연결되어 있지 않습니다.");
    }
    
    public void RestartGame()
    {
        Time.timeScale = 1f;
        
    }
    public void UpgradeTowers(RaceType raceType)
    {
        int upgradeCost = 20;

        if (gold < upgradeCost)
        {
            Debug.Log("자원이 부족합니다!");
            return;
        }

        gold -= upgradeCost;
        UpdateGoldUI();

        Tower[] allTowers = FindObjectsOfType<Tower>();
        foreach (var tower in allTowers)
        {
            TowerObject towerObj = tower.GetComponent<TowerObject>();
            if (towerObj != null && towerObj.towerData.raceType == raceType)
            {
                tower.Upgrade();
            }
        }
        raceUpgradeLevels[raceType]++;

        UpdateLevelUI(raceType);

        Debug.Log($"{raceType} 계열 타워 업그레이드 완료!");
    }
    private void UpdateLevelUI(RaceType raceType)
    {
        int level = raceUpgradeLevels[raceType];

        switch (raceType)
        {
            case RaceType.Ninja:
                ninjaLevelText.text = $"Lv.{level}";
                break;
            case RaceType.Gun:
                gunLevelText.text = $"Lv.{level}";
                break;
            case RaceType.Staff:
                staffLevelText.text = $"Lv.{level}";
                break;
        }
    }
    public int GetUpgradeLevel(RaceType raceType)
    {
        return raceUpgradeLevels[raceType];
    }
    
}