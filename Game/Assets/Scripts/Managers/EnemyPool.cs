using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour {

    private List<Enemy> backupEnemies;
    private List<Enemy> currentEnemies;

    [SerializeField]
    private Enemy enemyPrefab;
    [SerializeField]
    private int poolSize = 200;
    
    [SerializeField]
    private bool spawnMore;

    [SerializeField]
    private Transform poolContainer;

    private int enemyCount = 0;

	// Use this for initialization
	void Start () {
        currentEnemies = new List<Enemy>();
        backupEnemies = new List<Enemy>();

        for(int i = 0; i < poolSize; i++)
        {
            backupEnemies.Add(Spawn());
        }
	}

    private Enemy Spawn()
    {
        Enemy newObject = Instantiate(enemyPrefab);
        newObject.name = name + " " + enemyCount;
        enemyCount += 1;
        newObject.transform.SetParent(poolContainer, true);
        return newObject;
    }

    public void Sleep(Enemy obj)
    {
        currentEnemies.Remove(obj);
        //obj.Deactivate();
        obj.transform.SetParent(poolContainer, true);
        obj.gameObject.SetActive(false);
        backupEnemies.Add(obj);
    }

    public Enemy GetEnemy()
    {
        return WakeUp();
    }

    private Enemy WakeUp()
    {
        if (backupEnemies.Count <= 2)
        {
            if (spawnMore)
            {
                backupEnemies.Add(Spawn());
            }
        }
        Enemy newEnemy = null;
        if (backupEnemies.Count > 0)
        {
            newEnemy = backupEnemies[0];
            backupEnemies.RemoveAt(0);
            newEnemy.gameObject.SetActive(true);
            //newProjectile.Activate();
            currentEnemies.Add(newEnemy);
        }
        return newEnemy;
    }

    public List<Enemy> GetCurrentEnemies()
    {
        return currentEnemies;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
