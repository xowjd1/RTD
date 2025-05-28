using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] private float stageDuration = 20f;
    [SerializeField] private int totalStages = 50;

    private float timer;
    private int currentStage = 1;

    public int CurrentStage => currentStage;
    
    public static StageManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= stageDuration)
        {
            timer = 0f;
            currentStage++;

            if (currentStage > totalStages)
            {
                Debug.Log("게임 종료 또는 클리어");
            }
            else
            {
                Debug.Log($"스테이지 {currentStage} 시작!");
            }
        }
    }
}
