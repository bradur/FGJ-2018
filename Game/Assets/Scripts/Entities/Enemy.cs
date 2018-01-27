using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float nodeCheckInterval = 10f;

    private float lastCheck = 0f;
    private BuildableNode closest;
    private Rigidbody body;

    // Use this for initialization
    void Start()
    {
        gameObject.SetActive(false);
        body = GetComponent<Rigidbody>();
        closest = null;
        body.isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastCheck > nodeCheckInterval)
        {
            float distance = 99999f;

            lastCheck = Time.time;
            NodeManager nodemngr = GameManager.main.NodeManager;

            List<BuildableNode> nodes = nodemngr.GetNodes(false);

            foreach (BuildableNode node in nodes)
            {
                if (Vector3.Distance(transform.position, node.transform.position) < distance)
                {
                    distance = Vector3.Distance(transform.position, node.transform.position);
                    closest = node;
                }
            }
        }

        if(closest != null)
        {
            Vector3 dir = (closest.transform.position - transform.position).normalized*0.1f;
            //transform.position = transform.position + dir;
            body.AddForce(dir*.5f, ForceMode.VelocityChange);
            Debug.Log(dir);
        }
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
        body.isKinematic = true;
    }

    public void Activate()
    {
        gameObject.SetActive(true);
        body.isKinematic = false;
    }
}
