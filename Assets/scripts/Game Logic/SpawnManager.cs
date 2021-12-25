using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SpawnManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public GameObject enemyRobot;
    public GameObject gravityRobot;
    public List<GameObject> enemyInstances;
    public List<GameObject> gravityRobotInstances;
    HandgunController gunController;
    PlayerController playerController;
    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
        enemyInstances = new List<GameObject> {};
        gravityRobotInstances = new List<GameObject> {};
        addEnemyRobot(new Vector3(15, 15, 0));
        addGravityRobot(new Vector3(10, 20, 0));
    }

    // Update is called once per frame
    void Update()
    {
    }
    void addEnemyRobot(Vector3 pos)
    {
        GameObject newRobot = Instantiate(enemyRobot, pos, transform.rotation);

    }
    void addGravityRobot(Vector3 pos)
    {
        GameObject newRobot = Instantiate(gravityRobot, pos, transform.rotation);
        
    }

}