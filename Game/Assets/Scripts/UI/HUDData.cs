using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDData : MonoBehaviour
{

    [SerializeField]
    private Text txtCount;

    private int count;


    public void SetCount(int newCount)
    {
        count = newCount;
        txtCount.text = count.ToString();
    }

    public void AddToCount(int addition)
    {
        count += addition;
        txtCount.text = count.ToString();
    }

    public void SubtractFromCount(int subtraction)
    {
        count -= subtraction;
        txtCount.text = count.ToString();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
