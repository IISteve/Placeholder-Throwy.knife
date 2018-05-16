using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour {

    public GameObject targetObj;
    //no actual object is supposed to be for this in the inspector. public for debugging purposes.
    public Rigidbody tarObjRB;

    Vector3 randSpawnPoint;

    GameObject[] targetArr;
    GameObject newtar;
    bool isPaused = false;

    [HideInInspector]
    public bool confirmSpawn;
    public bool runDebugLogOfTargetList = false;

    //[HideInInspector] //not hidden for debugging 
    public int limiterzero = 0;
    //[HideInInspector] //not hidden for debugging 
    public int namingNum = -1;
    public int targetSpeed = 150;
    public int targetcountLimiter = 3;
    public int debugLoggingDelay = 1;


    //list of active targets
    public List<GameObject> targetList = new List<GameObject>();

    // Use this for initialization
    void Start() {

        tarObjRB = targetObj.GetComponent<Rigidbody>();

        //InvokeRepeating("SpawnTarget", 0f, 1f);
    }

    // Update is called once per frame
    void Update() {

        SpawnTarget();

        CheckPostDebLog();
    }

    private void FixedUpdate()
    {

    }

    void SpawnTarget()
    {
        if (limiterzero < targetcountLimiter /*testing -- > /*&& confirmSpawn == true*/)
        {
            SetRandSpawnPoint();

            newtar = Instantiate(targetObj, randSpawnPoint, Quaternion.identity);

            NameNewTar(newtar);
            //adds  the new target to the targetList
            targetList.Add(newtar);

            tarObjRB = newtar.GetComponent<Rigidbody>();

            tarObjRB.AddForce(-transform.right * targetSpeed);

            limiterzero++;
        }
    }

    //sets a random position for the new target to spawn
    void SetRandSpawnPoint()
    {
        randSpawnPoint = new Vector3(14, Random.Range(0f, 5f), Random.Range(-5f, 5f));
    }

    //names the newly instantiated target
    string NameNewTar(GameObject newTarget)
    {
        namingNum += 1;
        newTarget.gameObject.name = "Target" + namingNum;
        return name;
    }

    //checks if PostDebLog is supposed to run or not
    void CheckPostDebLog()
    {
        if (isPaused == true || !runDebugLogOfTargetList)
            return;

        StartCoroutine(PostDebLog());
    }

    //posts content of targetList to debug console
    IEnumerator PostDebLog()
    {
        for (int i = 0; i < targetList.Count; i++)
        {
            Debug.Log(targetList[i]);
        }
        Debug.Log(targetList.Count);

        isPaused = true;

        //halts this method for "x" amount of seconds
        yield return new WaitForSeconds(debugLoggingDelay);

        isPaused = false;
    }
}