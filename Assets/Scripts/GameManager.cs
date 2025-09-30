using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("ENEMIES")]
    public Transform spawnPoint;
    public Transform[] path01;
    public List<EnemyController> spawnedEnemies = new List<EnemyController>();

    [Header("TOWERS")]
    public TowerData[] availableTowers;
    public int playerGold = 50;

    TowerData _currentTowerSelected;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        StartCoroutine(SpawnEnemies(10));
    }

    private void Update()
    {
        if(_currentTowerSelected != null)
            if (Input.GetKeyDown(KeyCode.Escape)) _currentTowerSelected = null;

        if (Input.GetMouseButtonDown(0) && _currentTowerSelected != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag("PlacePos") && playerGold >= _currentTowerSelected.cost)
                    SetTower(hit.transform);
                else
                {
                    _currentTowerSelected = null;
                    Debug.Log("Not enough money");
                }
            }
        }
    }

    public IEnumerator SpawnEnemies(int enemiesPerRound)
    {
        while(enemiesPerRound > 0)
        {
            GameObject enemy = EnemiesPool.Instance.GetObject();

            enemy.transform.position = spawnPoint.position;
            enemy.transform.rotation = Quaternion.identity;

            EnemyController controller = enemy.GetComponent<EnemyController>();
            controller.SetWaypoints(path01);
            spawnedEnemies.Add(controller);
            enemiesPerRound--;

            yield return new WaitForSeconds(2);
        }
    }

    public void SelectedTower(TowerData tower) => _currentTowerSelected = tower;

    public void SetTower(Transform placePos)
    {
        playerGold -= _currentTowerSelected.cost;
        Instantiate(_currentTowerSelected.prefab, new Vector3(placePos.position.x, 2.5f, placePos.position.z), Quaternion.identity);
        _currentTowerSelected = null;
    }
}
