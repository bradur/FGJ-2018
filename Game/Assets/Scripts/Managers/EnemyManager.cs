using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    private float spawnRange;

    [SerializeField]
    private EnemyPool enemyPool;

    [SerializeField]
    private NodeManager nodeManager;

    [SerializeField]
    private GameManager gameManager;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnEnemy(Vector3 position)
    {
        Enemy enemy = enemyPool.GetEnemy();

        if (enemy != null)
        {
            enemy.transform.position = position;
            ResourceManager.main.Deposit(ResourceType.Enemy, 1);
        }

    }

    public void SpawnEnemyToOutside()
    {
        Vector2 pos = Random.insideUnitCircle.normalized * spawnRange;
        SpawnEnemy(new Vector3(pos.x, gameManager.Plane.transform.position.y+1.5f, pos.y));
    }

    public void KillEnemy(Enemy enemy)
    {
        enemyPool.Sleep(enemy);
    }
}
