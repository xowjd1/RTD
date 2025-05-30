using TMPro;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] private float stageDuration = 20f;
    [SerializeField] private int totalStages = 50;
    [SerializeField] private TMP_Text stageText;
    [SerializeField] private TMP_Text timeText;

    private float timer;
    private int currentStage = 1;

    public int CurrentStage => currentStage;
    
    public static StageManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateStageUI();
        UpdateTimeUI();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        float remaining = Mathf.Clamp(stageDuration - timer, 0f, stageDuration);
        UpdateTimeUI(remaining);

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
                UpdateStageUI();
            }
        }
    }
    
    private void UpdateStageUI()
    {
        if (stageText != null)
        {
            stageText.text = $"{currentStage} STAGE";
        }
    }
    private void UpdateTimeUI(float remainingTime = 0f)
    {
        if (timeText != null)
        {
            int timeInt = Mathf.CeilToInt(remainingTime);
            timeText.text = $"00 : {timeInt.ToString("00")}";
        }
    }

}
