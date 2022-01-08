using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddToSpawnManager : MonoBehaviour
{
    SpawnManager spawnManager;
    // Start is called before the first frame update
    void Start()
    {
        spawnManager = GameObject.FindWithTag("spawnmanager").GetComponent<SpawnManager>();
        spawnManager.allDynamicSprites.Add(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnDestroy() {
        spawnManager.allDynamicSprites.Remove(gameObject);
    }
}
