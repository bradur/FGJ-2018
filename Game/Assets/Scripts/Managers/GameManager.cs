using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private float SignalGoal = 10000f;

    private float signalSent = 0f;

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

    }

    // Update is called once per frame
    void Update()
    {
        float connections = nodeManager.GetConnections();
        //Debug.Log(connections);
        if (connections > 0)
        {
            signalSent += connections;
        }
        else if(connections == 0 && signalSent > 0)
        {
            //gameover
        }
    }
}
