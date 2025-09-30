using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public EnemyData myData;
    public Transform[] waypoints;

    Transform _currentWaypoint;
    int wayIndex = 0;

    private void Update()
    {
        Move();
    }

    void Move()
    {
        _currentWaypoint = waypoints[wayIndex];

        Vector3 dist = (_currentWaypoint.position - transform.position);
        if (dist.magnitude < myData.arriveRadius)
        {
            wayIndex++;
            if (wayIndex >= waypoints.Length)
            {
                GameManager.Instance.spawnedEnemies.Remove(this);
                EnemiesPool.Instance.ReturnObject(this.gameObject);
            }
        }

        transform.position += dist.normalized * myData.movementSpeed * Time.deltaTime;
        transform.right = dist.normalized;
    }

    public void SetWaypoints(Transform[] points) => waypoints = points;
}
