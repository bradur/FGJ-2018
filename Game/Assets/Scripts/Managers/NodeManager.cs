using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeManager : MonoBehaviour
{

    [SerializeField]
    private NodeProximityChecker nodePrefab;

    [SerializeField]
    private List<NodeProximityChecker> nodes = new List<NodeProximityChecker>();

    public static NodeManager main;

    [SerializeField]
    [Range(0.5f, 10f)]
    private float startBuildingDistance = 2.5f;

    public float StartBuildingDistance { get { return startBuildingDistance; } }

    [SerializeField]
    [Range(0.5f, 10f)]
    private float buildingDistance = 8.5f;

    public float BuildingDistance { get { return buildingDistance; } }

    void Awake()
    {
        main = this;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool IsBuildingAllowed(Vector3 position, Vector3 targetNodePosition)
    {
        Vector2 targetNodePosition2D = new Vector2(targetNodePosition.x, targetNodePosition.z);
        Vector2 position2D = new Vector2(position.x, position.z);
        if (Vector2.Distance(position2D, targetNodePosition2D) > buildingDistance)
        {
            return false;
        }
        foreach(NodeProximityChecker node in nodes)
        {
            Vector2 nodePosition2D = new Vector2(node.transform.position.x, node.transform.position.z);
            float distance = Vector2.Distance(position2D, nodePosition2D);
            if (distance <= startBuildingDistance)
            {
                return false;
            }
        }
        return true;
    }

    public NodeProximityChecker SpawnNode(Vector3 position)
    {
        NodeProximityChecker newNode = Instantiate(nodePrefab);
        newNode.gameObject.SetActive(true);
        newNode.transform.position = new Vector3(position.x, nodePrefab.transform.position.y, position.z);
        nodes.Add(newNode);
        return newNode;
    }

    public void SelectNode(NodeProximityChecker newNode)
    {
        foreach (NodeProximityChecker node in nodes)
        {
            node.Deselect();
        }
        newNode.Select();
    }

}
