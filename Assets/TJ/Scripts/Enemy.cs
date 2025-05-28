using UnityEngine;

public class Enemy : MonoBehaviour
{
    private WayPoint wayPoint;
    [SerializeField] private float speed = 2f;

    private int currentIndex = 0;
    private Vector3 targetPos;

    public void Initialize(WayPoint wp)
    {
        wayPoint = wp;
        currentIndex = 0;
        transform.position = wp.GetWaypointPosition(currentIndex);
        targetPos = wayPoint.GetWaypointPosition(currentIndex);
    }

    private void Update()
    {
        if (wayPoint == null) return;
        MoveToNextPoint();
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
    
}
