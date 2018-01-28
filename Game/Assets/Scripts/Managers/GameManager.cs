using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    [SerializeField]
    private float dataSendInterval = 0.5f;
    private float dataSendTimer = 0f;

    [SerializeField]
    private DataFilledIndicator dataFilledIndicator;

    [SerializeField]
    private EnemyManager enemyManager;

    public EnemyManager EnemyManager { get { return enemyManager; } }

    [SerializeField]
    public GameObject splash;

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
        if (splash != null)
        {
            Time.timeScale =0f;
        }
    }

    bool connected = false;
    bool gameOver = false;

    // Update is called once per frame
    void Update()
    {
        float connections = nodeManager.GetConnections();

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
            FinishLevel();
            Text txtMessage = finishedGameHUD.GetComponentInChildren<Text>();
            txtMessage.text = "GAME OVER!";
        }
        if (finished && Input.GetKeyUp(KeyCode.E))
        {
            NextLevel();
        }
        if (splash != null && Input.GetKeyUp(KeyCode.E))
        {
            splash.gameObject.SetActive(false);
            Time.timeScale = 1f;
        }
    }


    public void NextLevel()
    {
        if (nextLevel == -1 || gameOver)
        {
            Application.Quit();
        }
        else
        {
            SceneManager.LoadScene(nextLevel);
        }
    }

    private bool finished = false;
    [SerializeField]
    private GameObject finishedGameHUD;

    public void FinishLevel()
    {
        finishedGameHUD.SetActive(true);
        if (nextLevel == -1)
        {
            Text txtMessage = finishedGameHUD.GetComponentInChildren<Text>();
            txtMessage.text = "THE END!\nThanks for playing!";
        }
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
