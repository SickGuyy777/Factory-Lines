using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform[] waypoints;
    [SerializeField] float _movementSpeed;
    [SerializeField] float _arriveRadius;

    public float currentLife;

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
        if (dist.magnitude < _arriveRadius)
        {
            wayIndex++;
            if (wayIndex >= waypoints.Length) EnemiesPool.Instance.ReturnObject(this.gameObject);
        }

        transform.position += dist.normalized * _movementSpeed * Time.deltaTime;
        transform.right = dist.normalized;
    }

    public void SetWaypoints(Transform[] points) => waypoints = points;
}
