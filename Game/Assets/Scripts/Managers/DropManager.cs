using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropManager : MonoBehaviour {

    [SerializeField]
    DropPool dropPool;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }

    public void SpawnDrop(Vector3 position)
    {
        Drop drop = dropPool.GetDrop();

        if (drop != null)
        {
            drop.transform.position = position;
        }
    }
}
