using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPool : MonoBehaviour {

    private List<Drop> backupDrop;
    private List<Drop> currentDrop;

    [SerializeField]
    private Drop dropPrefab;
    [SerializeField]
    private int poolSize = 200;
    
    [SerializeField]
    private bool spawnMore;

    [SerializeField]
    private Transform poolContainer;

    private int dropCount = 0;

	// Use this for initialization
	void Start () {
        currentDrop = new List<Drop>();
        backupDrop = new List<Drop>();

        for(int i = 0; i < poolSize; i++)
        {
            backupDrop.Add(Spawn());
        }
	}

    private Drop Spawn()
    {
        Drop newObject = Instantiate(dropPrefab);
        newObject.name = name + " " + dropCount;
        dropCount += 1;
        newObject.transform.SetParent(poolContainer, true);
        return newObject;
    }

    public void Sleep(Drop obj)
    {
        currentDrop.Remove(obj);
        //obj.Deactivate();
        obj.transform.SetParent(poolContainer, true);
        obj.gameObject.SetActive(false);
        backupDrop.Add(obj);
    }

    public Drop GetDrop()
    {
        return WakeUp();
    }

    private Drop WakeUp()
    {
        if (backupDrop.Count <= 2)
        {
            if (spawnMore)
            {
                backupDrop.Add(Spawn());
            }
        }
        Drop newDrop = null;
        if (backupDrop.Count > 0)
        {
            newDrop = backupDrop[0];
            backupDrop.RemoveAt(0);
            newDrop.gameObject.SetActive(true);
            //newProjectile.Activate();
            currentDrop.Add(newDrop);
        }
        return newDrop;
    }

    public List<Drop> GetCurrentEnemies()
    {
        return currentDrop;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
