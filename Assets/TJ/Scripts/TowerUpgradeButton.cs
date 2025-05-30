using UnityEngine;

public class TowerUpgradeButton : MonoBehaviour
{
    public RaceType raceType;

    public void OnUpgradeButtonClick()
    {
        GameManager.Instance.UpgradeTowers(raceType);
    }
}