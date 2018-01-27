using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    [SerializeField]
    EnemyPool enemyPool;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SpawnEnemy(Vector3 position)
    {
        Enemy enemy = enemyPool.GetEnemy();

        if (enemy != null)
        {
            enemy.transform.position = position;
        }
    }
}
