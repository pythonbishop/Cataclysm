using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RunOnDeath : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject deathMenu;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDisable() {
        deathMenu.SetActive(true);
    }
}
