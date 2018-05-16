using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deleter : MonoBehaviour {

    public TargetSpawner targetSpawner;


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Target")
        {
            //lowers the "namingNum" variable so that the "targetList" list count is correct.
            //targetSpawner.namingNum = targetSpawner.namingNum - 1;

            //removes the gameobject form the "targetList" list.
            targetSpawner.targetList.Remove(other.gameObject);

            targetSpawner.limiterzero -= 1;
        }

        //kill gameobject.
        Destroy(other.gameObject);
    }
     
}