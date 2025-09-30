using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemy", menuName = "TowerDefense/Enemy")]
public class EnemyData : ScriptableObject
{
    public string enemyType;
    public GameObject prefab;
    public float movementSpeed;
    public float life;
    public float arriveRadius;
    public int damage;
}
