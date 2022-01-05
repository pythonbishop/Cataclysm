using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailgunController : MonoBehaviour
{
    // Start is called before the first frame update
    float angle;
    float currentDelay = 0.0f;
    bool mousePress;
    Vector3 gunToMouse;
    Vector3 mouseWorldPos;
    Vector3 rotatedBulletSpawn;
    Camera mainCamera;
    SpriteRenderer spriteRenderer;
    PlayerController playerController;
    SpriteRenderer playerSpriteRenderer;
    public Vector3 bulletSpawn;
    public Vector3 offsetRight;
    public Vector3 offsetLeft;
    public bool twoHanded;
    public GameObject bulletPrefab;
    public bool flipPlayerSprite;
    public float bulletDelay = 0.5f;
    void Start()
    {
        mainCamera = Camera.main;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sortingLayerName = "character";
        playerController = GetComponentInParent<PlayerController>();
        playerSpriteRenderer = gameObject.transform.parent.GetComponent<SpriteRenderer>();
        gunToMouse = new Vector3();
    }
    // Update is called once per frame
    void Update()
    {
        mouseWorldPos = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition[0], Input.mousePosition[1]));
        gunToMouse.Set(mouseWorldPos.x - transform.position[0], mouseWorldPos.y - transform.position[1], 0);
        angle = Vector3.Angle(Vector3.right, gunToMouse);
        
        rotatedBulletSpawn = Vector3.RotateTowards(bulletSpawn, gunToMouse, Mathf.Deg2Rad * angle, bulletSpawn.magnitude);
        rotatedBulletSpawn = Vector3.ClampMagnitude(rotatedBulletSpawn, bulletSpawn.magnitude) + transform.position;

        if (gunToMouse.y < 0)
        {
            angle = -angle;
        } 
        
        if ((int)gunToMouse.x > 0)
        {
            //gun face right
            spriteRenderer.flipX = false;
            transform.localPosition = offsetRight;
            transform.SetPositionAndRotation(transform.position, Quaternion.FromToRotation(Vector3.right, gunToMouse));

            if (flipPlayerSprite == true) {
                playerSpriteRenderer.flipX = false;
            }
            if (twoHanded == false) {
                spriteRenderer.sortingOrder = 2;
            }
        }

        else if ((int)gunToMouse.x < 0)
        {
            //gun face left
            spriteRenderer.flipX = true;
            transform.localPosition = offsetLeft;
            transform.SetPositionAndRotation(transform.position, Quaternion.FromToRotation(Vector3.left, gunToMouse));

            if (flipPlayerSprite == true) {
                playerSpriteRenderer.flipX = true;
            }

            if (twoHanded == false) {
                spriteRenderer.sortingOrder = 0;
            }
        }

        updateBulletDelay();
    }
    void spawnBullet()
    {
        float bulletAngle = angle;
        GameObject obj = Instantiate(bulletPrefab, rotatedBulletSpawn, Quaternion.FromToRotation(Vector3.right, gunToMouse));

        obj.GetComponent<BulletController>().direction = Quaternion.AngleAxis(bulletAngle, Vector3.forward) * Vector3.right;
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