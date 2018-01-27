using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViaToChild1 : MonoBehaviour
{

    //[SerializeField]
    LineRenderer viaRenderer;

    [SerializeField]
    List<GameObject> children;

    [SerializeField]
    GameObject parent;

    [SerializeField]
    GameObject plane;

    // Use this for initialization
    void Start()
    {
        viaRenderer = this.GetComponent<LineRenderer>();
        viaRenderer.alignment = LineAlignment.Local;
    }

    // Update is called once per frame
    void Update()
    {
        if (children == null || children.Count == 0)
        {
            viaRenderer.enabled = false;
        }
        else
        {
            foreach (GameObject child in children)
            {
                viaRenderer.enabled = true;
                viaRenderer.positionCount = 3;

                Vector3 p = transform.position;
                Vector3 c = child.transform.position;
                Vector3 b = plane.transform.position + new Vector3(0, 0.05f, 0);

                Vector3 tmp = Vector3.zero;
                Vector3 via0 = new Vector3(p.x, b.y, p.z);
                viaRenderer.SetPosition(0, via0);
                float xDist = p.x - c.x;
                float zDist = p.z - c.z;

                if (xDist >= 0)
                {
                    if (zDist >= 0) //top left quarter
                    {
                        tmp = new Vector3(-1, 0, -1);
                        if (xDist > zDist)
                        {
                            tmp *= zDist;
                        }
                        else
                        {
                            tmp *= xDist;
                        }
                    }
                    else //top right quarter
                    {
                        tmp = new Vector3(1, 0, -1);
                        if (Mathf.Abs(xDist) > Mathf.Abs(zDist))
                        {
                            tmp *= zDist;
                        }
                        else
                        {
                            tmp *= -xDist;
                        }
                    }
                }
                else
                {
                    if (zDist >= 0) //bottom left quarter
                    {
                        tmp = new Vector3(-1, 0, 1);
                        if (Mathf.Abs(xDist) > Mathf.Abs(zDist))
                        {
                            tmp *= -zDist;
                        }
                        else
                        {
                            tmp *= xDist;
                        }
                    }
                    else //bottom right quarter
                    {
                        tmp = new Vector3(1, 0, 1);
                        if (Mathf.Abs(xDist) > Mathf.Abs(zDist))
                        {
                            tmp *= -zDist;
                        }
                        else
                        {
                            tmp *= -xDist;

                        }
                    }
                }
                viaRenderer.SetPosition(1, tmp + via0);

                viaRenderer.SetPosition(2, new Vector3(c.x, b.y, c.z));
            }
        }
    }
}
