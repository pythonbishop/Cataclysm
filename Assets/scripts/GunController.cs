using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    // Start is called before the first frame update
    [HideInInspector]
    public float angle;
    [HideInInspector]
    public Vector3 bulletSpawn;
    [HideInInspector]
    public Vector3 gunToMouse;

    public float gunLength;
    public Vector3 offsetRight;
    public Vector3 offsetLeft;
    Vector3 mouseWorldPos;
    
    public GameObject bulletPrefab;
    Camera camera;
    SpriteRenderer spriteRenderer;
    PlayerController playerController;

    float currentDelay = 0.0f;
    public float bulletDelay = 0.5f; //move bullet spawn logic to player and enemy objects
    bool mousePress;
    void Start()
    {
        camera = Camera.main;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sortingLayerName = "character";
        playerController = GetComponentInParent<PlayerController>();
        gunToMouse = new Vector3();
    }
    // Update is called once per frame
    void Update()
    {
        mouseWorldPos = camera.ScreenToWorldPoint(new Vector3(Input.mousePosition[0], Input.mousePosition[1]));
        gunToMouse.Set(mouseWorldPos.x - transform.position[0], mouseWorldPos.y - transform.position[1], 0);
        angle = Vector3.Angle(Vector3.right, gunToMouse);

        bulletSpawn = transform.position + Vector3.Normalize(gunToMouse) * gunLength;

        if (gunToMouse.y < 0)
        {
            angle = -angle;
        } 
        
        if ((int)gunToMouse.x > 0)
        {
            //gun face right
            spriteRenderer.flipX = false;
            spriteRenderer.sortingOrder = 2;
            transform.localPosition = offsetRight;
            transform.SetPositionAndRotation(transform.position, Quaternion.FromToRotation(Vector3.right, gunToMouse));
        }

        else if ((int)gunToMouse.x < 0)
        {
            //gun face left
            spriteRenderer.flipX = true;
            spriteRenderer.sortingOrder = 0;
            transform.localPosition = offsetLeft;
            transform.SetPositionAndRotation(transform.position, Quaternion.FromToRotation(Vector3.left, gunToMouse));
        }

        updateBulletDelay();
    }
    void spawnBullet()
    {
        float bulletAngle = angle + Random.Range(-2, 2);
        GameObject obj = Instantiate(bulletPrefab, bulletSpawn, new Quaternion());

        obj.GetComponent<BulletController>().direction = Quaternion.AngleAxis(bulletAngle, Vector3.forward) * Vector3.right;
        obj.GetComponent<BulletController>().initialVelocity = playerController.velocity;
    }

    void updateBulletDelay()
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
}