using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenuController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void quitGame()
    {
        Application.Quit();
    }

    void restart()
    {
        SceneManager.LoadScene("Level 1");
    }
}
