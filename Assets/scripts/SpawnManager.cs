using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SpawnManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bulletPrefab;
    public GameObject weapon;
    public GameObject player;
    public GameObject[] enemyInstances;
    public GameObject[] gravityRobotInstances;
    GunController gunController;
    PlayerController playerController;
    public float bulletDelay = 0.5f; //move bullet spawn logic to player and enemy objects
    float currentDelay = 0.0f;
    Vector3 bulletSpawnPos;
    Vector3 randomOffset;
    bool mousePress;
    float bulletSpeed;
    void Start()
    {
        gunController = weapon.GetComponent<GunController>();
        playerController = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePress = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            mousePress = false;
        }

        if (currentDelay <= 0 & mousePress)
        {
            currentDelay = bulletDelay;
            spawnBullet();
        }
        
        currentDelay -= Time.deltaTime;

    }
    void spawnBullet()
    {
        bulletSpawnPos = gunController.bulletSpawn;
        float angle = gunController.angle + Random.Range(-2, 2);
        GameObject obj = Instantiate(bulletPrefab, bulletSpawnPos, new Quaternion());

        obj.GetComponent<BulletController>().direction = Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.right;
        obj.GetComponent<BulletController>().initialVelocity = playerController.velocity;
    }
}