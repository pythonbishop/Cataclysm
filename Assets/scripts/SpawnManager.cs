using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bullet;
    public GameObject weapon;
    Vector3 spawnPos;
    ScriptableObject objScript;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //GameObject obj = Instantiate(bullet, spawnPos, transform.rotation);
    }
}
