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
    private Color selectedNotBuildableColor;

    private Color defaultColor;

    [SerializeField]
    private ViaToChild pathIndicator;

    private Material deselected;

    [SerializeField]
    private Transform triggerDistanceIndicator;

    [SerializeField]
    private Transform buildDistanceIndicator;

    [SerializeField]
    private Transform nodeTransform;

    [SerializeField]
    private bool isRoot;

    public bool IsRoot { get { return isRoot; } }
    
    private bool buildable = true;
    public bool Buildable { get { return buildable; } }
    private bool isSelected = false;

    private SpriteRenderer triggerDistanceRenderer;

    [SerializeField]
    private BuildableType buildableType;

    // Use this for initialization
    void Start()
    {
        triggerDistanceRenderer = triggerDistanceIndicator.GetComponent<SpriteRenderer>();
        defaultColor = triggerDistanceRenderer.color;
        player = GameManager.main.Player;
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
        // if is within the range
        if (distance <= triggerDistance)
        {
            isCloseEnough = true;
            if (buildable)
            {
                if (!nodeBuildingMode.BuildingModeOn)
                {
                    triggerDistanceRenderer.color = defaultColor;
                }
                else
                {
                    triggerDistanceRenderer.color = selectedNotBuildableColor;
                }
                nodeBuildingMode.ShowBuildMessage(distance, this);
            }
            if (!isSelected)
            {
                Select();
            }
        }
        // if player has entered the range but is now outside the range
        else if (distance > triggerDistance)
        {
            if (isCloseEnough) {
                isCloseEnough = false;
                if (!nodeBuildingMode.BuildingModeOn)
                {
                    nodeBuildingMode.ClearCurrentNode(this);
                }
                else
                {
                    triggerDistanceRenderer.color = selectedNotBuildableColor;
                }
            }
            if (isSelected && !nodeBuildingMode.IsCurrentNode(this))
            {
                Deselect();
            }
        }
        // if player has entered the range and presses E
        if (buildable && isCloseEnough && Input.GetKeyUp(KeyCode.E) && !nodeBuildingMode.BuildingModeOn)
        {
            if (ResourceManager.main.CanBuild(buildableType))
            {
                ResourceManager.main.Build(buildableType);
                nodeBuildingMode.EnableBuildMode();
                buildDistanceIndicator.gameObject.SetActive(true);
                SetPathParent(player.gameObject);
            }
        }
    }

    public void SetUnbuildable()
    {
        triggerDistanceRenderer.color = selectedNotBuildableColor;
        buildable = false;
        Deselect();
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
        isSelected = true;

        triggerDistanceIndicator.gameObject.SetActive(true);
    }

    /// <summary>
    /// Remove highlight from node.
    /// </summary>
    public void Deselect()
    {
        isSelected = false;
        buildDistanceIndicator.gameObject.SetActive(false);
        triggerDistanceIndicator.gameObject.SetActive(false);
    }
}
