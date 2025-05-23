using UnityEngine;


public enum RaceType
{
    Ninja,
    Gun
}
[CreateAssetMenu(fileName = "TowerData", menuName = "Tower/New Tower")]
public class TowerData : ScriptableObject
{
    public string towerName;
    public Sprite icon;
    public GameObject prefab;
    public int level;
    public float damage;
    public float range;
    public float attackSpeed;
    public TowerType type;
    public RaceType raceType;
    
    public enum TowerType { Projectile, AoE, Buff }
}
