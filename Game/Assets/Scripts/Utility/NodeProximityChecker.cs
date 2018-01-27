using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeProximityChecker : MonoBehaviour
{

    private Transform player;

    [SerializeField]
    [Range(0.5f, 10f)]
    private float triggerDistance = 1f;

    private bool isCloseEnough = false;

    private NodeBuildingMode nodeBuildingMode;

    [SerializeField]
    private Material selected;

    private Material deselected;

    private MeshRenderer meshRenderer;

    // Use this for initialization
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        player = GameManager.main.Player;
        deselected = meshRenderer.material;
        nodeBuildingMode = player.GetComponent<NodeBuildingMode>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position2D = new Vector2(transform.position.x, transform.position.z);
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
            nodeBuildingMode.ClearCurrentNode(this);
            nodeBuildingMode.ExitBuildRange();
        }
        // if player has entered the range and presses E
        if (isCloseEnough && Input.GetKeyUp(KeyCode.E))
        {
            nodeBuildingMode.EnableBuildMode();
        }
    }

    /// <summary>
    /// Highlight node (highlighted node will be used for build mode)
    /// </summary>
    public void Select()
    {
        meshRenderer.material = selected;
    }

    /// <summary>
    /// Remove highlight from node.
    /// </summary>
    public void Deselect()
    {
        meshRenderer.material = deselected;
    }
}
