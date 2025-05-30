using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private WayPoint wayPoint;
    [SerializeField] private float speed = 2f;

    private int currentIndex = 0;
    private Vector3 targetPos;
    
    public int hp;
    public int maxHp;
    
    public TMP_Text hpText;

    private void Start()
    {
        UpdateHpText();
    }
    
    public void Initialize(WayPoint wp)
    {
        wayPoint = wp;
        currentIndex = 0;
        transform.position = wp.GetWaypointPosition(currentIndex);
        targetPos = wayPoint.GetWaypointPosition(currentIndex);
        
        SetHpByStage();
    }

    private void Update()
    {
        if (wayPoint == null) return;
        MoveToNextPoint();
        
        if (hpText != null)
        {
            hpText.transform.rotation = Quaternion.LookRotation(Vector3.forward);
        }
    }

    private void SetHpByStage()
    {
        int stage = StageManager.Instance.CurrentStage;
        maxHp = 50 + (stage - 1) * 100;
        hp = maxHp;
        UpdateHpText();
    }
    
    private void MoveToNextPoint()
    {
        Vector3 dir = (targetPos - transform.position).normalized;
        transform.position += dir * speed * Time.deltaTime;

        if (Vector3.Distance(transform.position, targetPos) < 0.1f)
        {
            currentIndex = (currentIndex + 1) % wayPoint.Points.Length;
            targetPos = wayPoint.GetWaypointPosition(currentIndex);
        }
    }
    private void OnEnable()
    {
        maxHp = hp;
        
        if (GameManager.Instance != null)
            GameManager.Instance.ChangeMonsterCount(+1);
    }

    private void OnDisable()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.ChangeMonsterCount(-1);
    }

    public void TakeDamage(int amount)
    {
        hp -= amount;
        if (hp < 0) hp = 0;

        UpdateHpText();

        if (hp <= 0)
        {
            GameManager.Instance.RewardCurrencyForKill();
            gameObject.SetActive(false);
        }
    }

    private void UpdateHpText()
    {
        if (hpText != null)
            hpText.text = $"{hp}";
    }
    
}
