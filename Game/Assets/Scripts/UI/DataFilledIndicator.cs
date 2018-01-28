using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataFilledIndicator : MonoBehaviour
{

    [SerializeField]
    private List<Image> images = new List<Image>();

    private float fillStep = 0.1f;

    private float timer = 0f;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        /*if (timer <= 1f)
        {
            timer += Time.deltaTime;
        } else
        {
            AddData(1);
            timer = 0f;
        }*/
    }


    public void AddData(float amount)
    {
        foreach(Image newImage in images)
        {
            if (newImage.fillAmount < 1f)
            {
                newImage.fillAmount += amount * fillStep;
                return;
            }
        }
        Debug.Log("WhollyFilled!");
    }
}
