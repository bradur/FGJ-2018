using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViaToChild : MonoBehaviour
{

    [SerializeField]
    LineRenderer viaRenderer;

    [SerializeField]
    List<GameObject> children;

    [SerializeField]
    GameObject parent;

    GameObject plane;

    [SerializeField]
    GameObject ownVia;

    public void SetParent(GameObject newParent)
    {
        parent = newParent;
    }

    // Use this for initialization
    void Start()
    {
        plane = GameManager.main.Plane;
        viaRenderer = ownVia.GetComponent<LineRenderer>();
        viaRenderer.alignment = LineAlignment.Local;
    }

    // Update is called once per frame
    void Update()
    {
        if (parent == null)
        {
            viaRenderer.enabled = false;
        }
        else
        {
            viaRenderer.enabled = true;
            viaRenderer.positionCount = 3;

            Vector3 p = parent.transform.position;
            Vector3 c = transform.position;
            Vector3 b = plane.transform.position + new Vector3(0, 0.05f, 0);

            Vector3 midPoint = Vector3.zero;
            Vector3 firstPoint = new Vector3(p.x, b.y, p.z);
            float xDist = p.x - c.x;
            float zDist = p.z - c.z;

            //calculate mid point of the via
            if (xDist >= 0)
            {
                if (zDist >= 0) //top left quarter
                {
                    midPoint = new Vector3(-1, 0, -1);
                    if (xDist > zDist)
                    {
                        midPoint *= zDist;
                    }
                    else
                    {
                        midPoint *= xDist;
                    }
                }
                else //top right quarter
                {
                    midPoint = new Vector3(1, 0, -1);
                    if (Mathf.Abs(xDist) > Mathf.Abs(zDist))
                    {
                        midPoint *= zDist;
                    }
                    else
                    {
                        midPoint *= -xDist;
                    }
                }
            }
            else
            {
                if (zDist >= 0) //bottom left quarter
                {
                    midPoint = new Vector3(-1, 0, 1);
                    if (Mathf.Abs(xDist) > Mathf.Abs(zDist))
                    {
                        midPoint *= -zDist;
                    }
                    else
                    {
                        midPoint *= xDist;
                    }
                }
                else //bottom right quarter
                {
                    midPoint = new Vector3(1, 0, 1);
                    if (Mathf.Abs(xDist) > Mathf.Abs(zDist))
                    {
                        midPoint *= -zDist;
                    }
                    else
                    {
                        midPoint *= -xDist;

                    }
                }
            }
            
            viaRenderer.SetPosition(0, firstPoint);
            viaRenderer.SetPosition(1, midPoint + firstPoint);
            viaRenderer.SetPosition(2, new Vector3(c.x, b.y, c.z));
        }
    }
}
