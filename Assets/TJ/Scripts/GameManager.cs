using UnityEngine;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public TowerDataBase towerDB;

    private void Awake()
    {
        Instance = this;
    }
}