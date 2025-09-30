using System.Collections.Generic;
using UnityEngine;

public class ArcherController : MonoBehaviour
{
    public TowerData myData;

    List<EnemyController> _inZoneEnemies = new List<EnemyController>();
    public EnemyController _currentTarget;

    private void Update()
    {
        DetectEnemy();
    }

    void DetectEnemy()
    {
        if (GameManager.Instance.spawnedEnemies != null)
        {
            foreach (var item in GameManager.Instance.spawnedEnemies)
            {
                Vector3 dist = item.transform.position - transform.position;
                if (dist.magnitude <= myData.range)
                {
                    if (!_inZoneEnemies.Contains(item))
                        _inZoneEnemies.Add(item);
                }
                else
                {
                    if (_inZoneEnemies.Contains(item))
                        _inZoneEnemies.Remove(item);
                }
            }
        }

        if (_inZoneEnemies.Count > 0)
            _currentTarget = _inZoneEnemies[0];
        else _currentTarget = null;
    }
}
