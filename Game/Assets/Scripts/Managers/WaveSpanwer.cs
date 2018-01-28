using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpanwer : MonoBehaviour
{
    [SerializeField]
    EnemyManager enemyManager;

    [SerializeField]
    private float timeBetweenWaves;

    [SerializeField]
    private List<float> waves;

    [SerializeField]
    private bool infiniteWaves = false;


    private float lastWaveTime = 0f;
    private int currentWave = 0;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.main.GetConnections() > 0 || GameManager.main.GetSignalSent() > 0)
        {
            if (Time.time - lastWaveTime > timeBetweenWaves)
            {
                if (!infiniteWaves)
                {
                    //spawn waves
                    if (currentWave + 1 <= waves.Count)
                    {
                        for (int i = 0; i < waves[currentWave]; i++)
                        {
                            enemyManager.SpawnEnemyToOutside();
                        }
                    }

                    currentWave++;
                    lastWaveTime = Time.time;
                }
                else
                {
                    currentWave++;
                    lastWaveTime = Time.time;
                    for (int i = 0; i < currentWave; i++)
                    {
                        enemyManager.SpawnEnemyToOutside();
                    }
                }
            }
        }
    }
}
