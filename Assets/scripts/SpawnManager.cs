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
    PlayerController playerController;
    GravityRobotController gRobotController;
    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
        gRobotController = gravityRobot.GetComponent<GravityRobotController>();
        enemyInstances = new List<GameObject> { };
        gravityRobotInstances = new List<GameObject> { };
        allDynamicSprites.Add(player);
        
        testSpwan();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            testSpwan();
        }
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

    void testSpwan()
    {
        for (int x = 0; x < 100; x += 10)
        {
            for (int y = 0; y < 100; y += 10)
            {
                addEnemyRobot(new Vector3(x+15, y-50, 0));
            }
        }
    }
}