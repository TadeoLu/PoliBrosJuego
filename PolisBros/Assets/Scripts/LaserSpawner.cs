using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject laser;
    private Laser laserScript;
    void Start()
    {   
        laserScript = laser.GetComponent<Laser>();
        StartCoroutine(SpawnLaserLoop());
    }

    // Update is called once per frame

    IEnumerator SpawnLaserLoop()
    {
        while(true){
            yield return StartCoroutine(laserScript.SpawnLaser());
        }
    }
}
