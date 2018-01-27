using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    [SerializeField]
    EnemyManager enemyManager;

    [SerializeField]
    DropManager dropManager;

    float testInterval = 2f;
    float lastTime = 0f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - lastTime > testInterval) {
            lastTime = Time.time;
            Vector3 random = new Vector3(Random.value * 5, 1.05f, Random.value * 5);
            enemyManager.SpawnEnemy(random);

            random = new Vector3(Random.value * 5, 1.05f, Random.value * 5);
            dropManager.SpawnDrop(random);
            Debug.Log("test");
        }
    }
}
