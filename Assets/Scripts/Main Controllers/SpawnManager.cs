using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SpawnManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject enemyRobot;
    public GameObject gravityRobot;
    public List<GameObject> allDynamicSprites;
    GameObject player;
    PlayerController playerController;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerController = player.GetComponent<PlayerController>();

        GameObject[] items = GameObject.FindGameObjectsWithTag("item");

        foreach (var item in items)
        {
            Destroy(item);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void addEnemyRobot(Vector3 pos)
    {
        GameObject newRobot = Instantiate(enemyRobot, pos, transform.rotation);
    }
    public void addGravityRobot(Vector3 pos)
    {
        GameObject newRobot = Instantiate(gravityRobot, pos, transform.rotation);
    }

    public void testSpwan()
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