using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeBuildingMode : MonoBehaviour
{

    [SerializeField]
    private bool buildingModeOn = false;

    public bool BuildingModeOn { get { return buildingModeOn; } }

    [SerializeField]
    private GameObject uiBuildModeIndicator;

    [SerializeField]
    private GameObject uiCanBuildIndicator;

    private BuildableNode currentNode;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (buildingModeOn && currentNode != null && Input.GetKeyUp(KeyCode.E))
        {
            /*Vector2 currentNodePosition = new Vector2(currentNode.transform.position.x, currentNode.transform.position.z);
            Vector2 currentPosition = new Vector2(transform.position.x, transform.position.z);
            float currentNodeDistance = Vector2.Distance(currentPosition, currentNodePosition);*/
            bool allowBuild = NodeManager.main.IsBuildingAllowed(transform.position, currentNode.transform.position);
            if (allowBuild)
            {
                NodeManager.main.DeselectAll();
                BuildableNode newNode = NodeManager.main.SpawnNode(transform.position);
                //currentNode.SetPathParent(newNode.gameObject);
                newNode.SetPathParent(currentNode.gameObject);
                currentNode.SetPathChild(newNode.gameObject);
                //currentNode.SetUnbuildable();
                currentNode.Deselect();
                Disable();
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void Disable()
    {
        buildingModeOn = false;
        uiBuildModeIndicator.SetActive(false);
        GameManager.main.HideBuildIndicator();
    }

    /// <summary>
    /// Show indicator on top of current node.
    /// Allow player to create another node that will be linked to this.
    /// </summary>
    public void EnableBuildMode()
    {
        buildingModeOn = true;
        uiBuildModeIndicator.SetActive(true);
        uiCanBuildIndicator.SetActive(false);
        GameManager.main.ShowBuildIndicator(currentNode.transform.position);
    }

    /// <summary>
    /// Show message that indicates player may go into building mode.
    /// Check if given node is closer than the current one, if so, set that as current.
    /// </summary>
    /// <param name="distance"></param>
    /// <param name="node"></param>
    public void ShowBuildMessage(float distance, BuildableNode node)
    {
        if (!buildingModeOn)
        {
            if (currentNode == null)
            {
                currentNode = node;
                currentNode.Select();
                uiCanBuildIndicator.SetActive(true);
            }
            else
            {
                Vector2 currentNodePosition = new Vector2(currentNode.transform.position.x, currentNode.transform.position.z);
                Vector2 currentPosition = new Vector2(transform.position.x, transform.position.z);
                float currentNodeDistance = Vector2.Distance(currentPosition, currentNodePosition);
                if (currentNodeDistance > distance)
                {
                    currentNode.Deselect();
                    currentNode = node;
                    currentNode.Select();
                    uiCanBuildIndicator.SetActive(true);
                }
            }
        }
    }

    public bool IsCurrentNode(BuildableNode node)
    {
        return currentNode == node;
    }

    /// <summary>
    /// Deselect current node and disable "can build" indicator.
    /// </summary>
    /// <param name="node"></param>
    public void ClearCurrentNode(BuildableNode node)
    {
        if (currentNode == node)
        {
            uiCanBuildIndicator.SetActive(false);
            currentNode.Deselect();
            currentNode = null;
        }
    }

    /*public void HideBuildMessage()
    {
        uiCanBuildIndicator.SetActive(false);
    }*/
}
