using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float nodeCheckInterval = 10f;

    [SerializeField]
    private float health;

    [SerializeField]
    private float damage;
    [SerializeField]
    private float hitInterval;
    [SerializeField]
    private bool isAttacking; //can attack if true

    private float lastNodeCheck = 0f;
    private float lastHit = 0f;
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
        if (Time.time - lastNodeCheck > nodeCheckInterval)
        {
            float distance = 99999f;

            lastNodeCheck = Time.time;
            NodeManager nodemngr = GameManager.main.NodeManager;

            List<BuildableNode> nodes = nodemngr.GetNodes(false);

            foreach (BuildableNode node in nodes)
            {
                if (node == null) continue;

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
            body.AddForce(dir*.5f, ForceMode.VelocityChange);
        }
    }

    public void GetHit()
    {
        Deactivate();
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
        body.isKinematic = true;
        isAttacking = false;
    }

    public void Activate()
    {
        gameObject.SetActive(true);
        body.isKinematic = false;
        isAttacking = true;
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "EnergyNode")
        {
            Vector3 dir = (transform.position - closest.transform.position).normalized;
            body.AddForce(dir * 2f, ForceMode.Impulse);
            if (isAttacking && (Time.time - lastHit > hitInterval))
            {
                lastHit = Time.time;
                BuildableNode node = collision.gameObject.transform.parent.GetComponent<BuildableNode>();
                if(node != null)
                {
                    node.ReduceHealth(damage);
                }
            }
        }
    }
}
