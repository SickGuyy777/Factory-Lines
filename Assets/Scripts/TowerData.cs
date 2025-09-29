using UnityEngine;

[CreateAssetMenu(fileName = "NewTower", menuName = "TowerDefense/Tower")]
public class TowerData : ScriptableObject
{
    public string towerType;
    public GameObject prefab;
    public int cost;
    public float range;
    public float fireRate;
    public int damage;
}