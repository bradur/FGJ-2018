using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager main;

    [SerializeField]
    private Transform player;

    public Transform Player { get { return player; } }

    [SerializeField]
    private GameObject uiBuildMessage;

    public GameObject UIBuildMessage { get { return uiBuildMessage; } }

    [SerializeField]
    private GameObject buildIndicator;

    [SerializeField]
    private GameObject plane;

    public GameObject Plane { get { return plane; } }

    [SerializeField]
    private NodeManager nodeManager;

    public NodeManager NodeManager { get { return nodeManager; } }

    [SerializeField]
    private float SignalGoal = 100f;

    private float signalSent = 0f;

    [SerializeField]
    private int nextLevel;

    [SerializeField]
    private int enemyDropAmount;

    public int EnemyDropAmount { get { return enemyDropAmount; } }
    
    private float dataSendInterval = 0.5f;
    private float dataSendTimer = 0f;

    [SerializeField]
    private DataFilledIndicator dataFilledIndicator;

    [SerializeField]
    private EnemyManager enemyManager;

    public EnemyManager EnemyManager { get { return enemyManager; } }

    void Awake()
    {
        main = this;
    }

    public void ShowBuildIndicator(Vector3 showPosition)
    {
        buildIndicator.SetActive(true);
        buildIndicator.transform.position = new Vector3(showPosition.x, buildIndicator.transform.position.y, showPosition.z);
    }

    public void HideBuildIndicator()
    {
        buildIndicator.SetActive(false);
    }

    // Use this for initialization
    void Start()
    {
        Time.timeScale = 1f;
    }

    bool connected = false;

    // Update is called once per frame
    void Update()
    {
        float connections = nodeManager.GetConnections();
        //Debug.Log(connections);
        if (connections > 0)
        {
            if (dataSendTimer <= dataSendInterval)
            {
                dataSendTimer += Time.deltaTime;
            }
            if (dataSendTimer >= dataSendInterval)
            {
                dataSendTimer = 0f;
                signalSent += connections;
                dataFilledIndicator.AddData(connections);
            }
        }
        else if(connections == 0 && signalSent > 0)
        {
            //gameover
        }
        if (finished && Input.GetKeyUp(KeyCode.E))
        {
            NextLevel();
        }
    }


    public void NextLevel()
    {
        if (nextLevel == -1)
        {
            Debug.Log("Win!");
            Application.Quit();
        }
        SceneManager.LoadScene(nextLevel);
    }

    private bool finished = false;
    [SerializeField]
    private GameObject finishedGameHUD;

    public void FinishLevel()
    {
        finishedGameHUD.SetActive(true);
        Time.timeScale = 0f;
        finished = true;
    }

    public float GetConnections()
    {
        return nodeManager.GetConnections();
    }

    public float GetSignalSent()
    {
        return signalSent;
    }
}
