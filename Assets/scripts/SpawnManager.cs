using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SpawnManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bulletPrefab;
    public GameObject weapon;
    public GameObject player;
    public float bulletDelay = 0.5f;
    GunController gunController;
    PlayerController playerController;
    float currentDelay = 0.0f;
    Vector3 bulletSpawnPos;
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
        Vector3 angle = Vector3.Normalize(new Vector3(Input.mousePosition[0] - Screen.width/2, Input.mousePosition[1] - Screen.height/2, 0));
        bulletSpawnPos = gunController.bulletSpawn;
        GameObject obj = Instantiate(bulletPrefab, bulletSpawnPos, new Quaternion());
        obj.GetComponent<BulletController>().direction = angle;
        obj.GetComponent<BulletController>().initialVelocity = playerController.velocity;
    }
}