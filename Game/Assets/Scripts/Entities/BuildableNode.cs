using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildableNode : MonoBehaviour
{

    private Transform player;

    private float triggerDistance;

    private float buildDistance;

    public float BuildDistance { get { return buildDistance; } }

    private bool isCloseEnough = false;

    private NodeBuildingMode nodeBuildingMode;

    [SerializeField]
    private Material selected;

    [SerializeField]
    private ViaToChild pathIndicator;

    private Material deselected;

    private MeshRenderer meshRenderer;

    [SerializeField]
    private Transform triggerDistanceIndicator;

    [SerializeField]
    private Transform buildDistanceIndicator;

    [SerializeField]
    private Transform nodeTransform;

    [SerializeField]
    private bool isRoot;

    public bool IsRoot { get { return isRoot; } }

    // Use this for initialization
    void Start()
    {
        meshRenderer = nodeTransform.GetComponent<MeshRenderer>();
        player = GameManager.main.Player;
        deselected = meshRenderer.material;
        nodeBuildingMode = player.GetComponent<NodeBuildingMode>();
        triggerDistance = NodeManager.main.StartBuildingDistance;
        buildDistance = NodeManager.main.BuildingDistance;
        triggerDistanceIndicator.localScale = new Vector3(triggerDistance * 2, triggerDistance * 2, triggerDistanceIndicator.localScale.y);
        buildDistanceIndicator.localScale = new Vector3(buildDistance * 2, buildDistance * 2, buildDistanceIndicator.localScale.y);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position2D = new Vector2(nodeTransform.position.x, nodeTransform.position.z);
        Vector2 playerPosition2D = new Vector2(player.position.x, player.position.z);
        float distance = Vector2.Distance(position2D, playerPosition2D);
        // if player hasn't entered the range before but is now within the range
        if (distance <= triggerDistance)
        {
            isCloseEnough = true;
            nodeBuildingMode.ShowBuildMessage(distance, this);
            nodeBuildingMode.EnterBuildRange();
        }
        // if player has entered the range but is now outside the range
        else if (isCloseEnough && distance > triggerDistance)
        {
            isCloseEnough = false;
            if (!nodeBuildingMode.BuildingModeOn)
            {
                nodeBuildingMode.ClearCurrentNode(this);
            }
            //nodeBuildingMode.ClearCurrentNode(this);
            nodeBuildingMode.ExitBuildRange();
        }
        // if player has entered the range and presses E
        if (isCloseEnough && Input.GetKeyUp(KeyCode.E) && !nodeBuildingMode.BuildingModeOn)
        {
            nodeBuildingMode.EnableBuildMode();
            buildDistanceIndicator.gameObject.SetActive(true);
            SetPathParent(player.gameObject);
        }
    }

    public void SetPathParent(GameObject parent)
    {
        pathIndicator.SetParent(parent);
    }

    /// <summary>
    /// Highlight node (highlighted node will be used for build mode)
    /// </summary>
    public void Select()
    {
        meshRenderer.material = selected;
        triggerDistanceIndicator.gameObject.SetActive(true);
    }

    /// <summary>
    /// Remove highlight from node.
    /// </summary>
    public void Deselect()
    {
        meshRenderer.material = deselected;
        buildDistanceIndicator.gameObject.SetActive(false);
        triggerDistanceIndicator.gameObject.SetActive(false);
    }
}
