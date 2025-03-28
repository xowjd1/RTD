using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static Action OnEndReached;
    
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private WayPoint waypoint;

    public Vector3 CurrentPointPosition => waypoint.GetWaypointPosition(_currentWaypointIndex);
    
    private int _currentWaypointIndex = 0;

    private void Start()
    {
        _currentWaypointIndex = 0;
    }

    private void Update()
    {
        Move();
        if (CurrentPointPositionReached())
        {
            UpdateCurrentPointIndex();
        }
    }
    
    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, CurrentPointPosition, moveSpeed * Time.deltaTime);
    }

    private bool CurrentPointPositionReached()
    {
        float distanceToNextPointPosition = (transform.position - CurrentPointPosition).magnitude;
        if (distanceToNextPointPosition < 0.1f)
        {
            return true;
        }

        return false;
    }

    private void UpdateCurrentPointIndex()
    {
        int lastWaypointIndex = waypoint.Points.Length - 1;
        if (_currentWaypointIndex < lastWaypointIndex)
        {
            _currentWaypointIndex++;
        }
        else
        {
            ReturnEnemyToPool();
        }
        
    }

    private void ReturnEnemyToPool()
    {
        /*
        if (OnEndReached != null)
        {
            OnEndReached.Invoke();
        }
        */
        OnEndReached?.Invoke();
        ObjectPooler.ReturnToPool(gameObject);
    }
    
    
}
