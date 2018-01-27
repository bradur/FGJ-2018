using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeCollider : MonoBehaviour
{

    [SerializeField]
    private BuildableNode parentNode;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        //if(collision.gameObject.tag == "Enemy")
    }
}
