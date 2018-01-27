using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{

    public static HUDManager main;

    private void Awake()
    {
        main = this;
    }

    [SerializeField]
    private HUDData hudData;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void WithDraw(ResourceType resourceType, int amount)
    {
        if (resourceType == ResourceType.Data)
        {
            hudData.SubtractFromCount(amount);
        }
    }

    public void Deposit(ResourceType resourceType, int amount)
    {
        if (resourceType == ResourceType.Data)
        {
            hudData.AddToCount(amount);
        }
    }

}
