using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceType
{
    None,
    Data
}

public enum BuildableType
{
    None,
    EnergyNode,
    DefensiveNode
}

public class ResourceManager : MonoBehaviour
{

    public static ResourceManager main;

    [SerializeField]
    private List<GameResource> resources = new List<GameResource>();

    [SerializeField]
    private List<GameBuildableNode> buildables = new List<GameBuildableNode>();

    void Awake()
    {
        main = this;
    }

    public bool CanBuild(BuildableType buildableType)
    {
        foreach (GameBuildableNode buildableNode in buildables)
        {
            if (buildableType == buildableNode.buildableType && CanWithdraw(buildableNode.costType, buildableNode.cost))
            {
                return true;
            }
        }
        return false;
    }

    public void Build(BuildableType buildableType)
    {
        foreach (GameBuildableNode buildableNode in buildables)
        {
            if (buildableType == buildableNode.buildableType && CanWithdraw(buildableNode.costType, buildableNode.cost))
            {
                WithDraw(buildableNode.costType, buildableNode.cost);
            }
        }
    }

    public bool CanWithdraw(ResourceType resourceType, int amount)
    {
        foreach(GameResource resource in resources)
        {
            if (resourceType == resource.resourceType && (amount <= resource.count))
            {
                return true;
            }
        }
        return false;
    }

    public void WithDraw(ResourceType resourceType, int amount)
    {
        foreach (GameResource resource in resources)
        {
            if (resourceType == resource.resourceType && amount <= resource.count)
            {
                resource.count -= amount;
                HUDManager.main.WithDraw(resourceType, amount);
            }
        }
    }

    public void Deposit(ResourceType resourceType, int amount)
    {
        foreach (GameResource resource in resources)
        {
            if (resourceType == resource.resourceType && amount <= resource.count)
            {
                HUDManager.main.Deposit(resourceType, amount);
                resource.count += amount;
            }
        }
    }


    // Use this for initialization
    void Start()
    {
        foreach(GameResource resource in resources)
        {
            HUDManager.main.Deposit(resource.resourceType, resource.count);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

[System.Serializable]
public class GameResource: System.Object
{
    public ResourceType resourceType;
    public int count;
}

[System.Serializable]
public class GameBuildableNode : System.Object
{
    public BuildableType buildableType;
    public ResourceType costType;
    public int cost;
}