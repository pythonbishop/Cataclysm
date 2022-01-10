using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddToSpawnManager : MonoBehaviour
{
    //adds gameObject to SpawnManager.allDynamicSprites on creation and removes them when destroyed
    SpawnManager spawnManager;
    void Start()
    {
        spawnManager = GameObject.FindWithTag("spawnmanager").GetComponent<SpawnManager>();
        spawnManager.allDynamicSprites.Add(gameObject);
    }
    void Update()
    {

    }

    private void OnDestroy()
    {
        spawnManager.allDynamicSprites.Remove(gameObject);
    }
}
