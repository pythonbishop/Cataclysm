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
    public List<GameObject> allDynamicSprites;
    HandgunController gunController;
    PlayerController playerController;
    GravityRobotController gRobotController;
    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
        gRobotController = gravityRobot.GetComponent<GravityRobotController>();
        enemyInstances = new List<GameObject> { };
        gravityRobotInstances = new List<GameObject> { };
        allDynamicSprites.Add(player);
        addEnemyRobot(new Vector3(15, 15, 0));
        addEnemyRobot(new Vector3(15, 20, 0));
        addEnemyRobot(new Vector3(20, 15, 0));
        addEnemyRobot(new Vector3(20, 20, 0));
        addGravityRobot(new Vector3(30, 30, 0));
    }

    // Update is called once per frame
    void Update()
    {

    }
    void addEnemyRobot(Vector3 pos)
    {
        GameObject newRobot = Instantiate(enemyRobot, pos, transform.rotation);
        enemyInstances.Add(newRobot);
        allDynamicSprites.Add(newRobot);

    }
    void addGravityRobot(Vector3 pos)
    {
        GameObject newRobot = Instantiate(gravityRobot, pos, transform.rotation);
        gravityRobotInstances.Add(newRobot);
        allDynamicSprites.Add(newRobot);

    }

}