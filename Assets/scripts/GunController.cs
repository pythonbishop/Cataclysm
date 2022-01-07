using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
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
    Rigidbody2D parentRbody;
    public Vector3 bulletSpawn;
    public Vector3 offsetRight;
    public Vector3 offsetLeft;
    public float bulletSpeed;
    public bool twoHanded;
    public float speedVariation;
    public GameObject bulletPrefab;
    public bool flipPlayerSprite;
    public float bulletDelay;
    public float spreadShotgun;
    public float bulletSpread;
    public int numShot;
    void Start()
    {
        mainCamera = Camera.main;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sortingLayerName = "character";
        playerController = GetComponentInParent<PlayerController>();
        playerSpriteRenderer = gameObject.transform.parent.GetComponent<SpriteRenderer>();
        gunToMouse = new Vector3();
        parentRbody = transform.parent.gameObject.GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        mouseWorldPos = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition[0], Input.mousePosition[1]));
        gunToMouse.Set(mouseWorldPos.x - transform.position[0], mouseWorldPos.y - transform.position[1], 0);
        angle = Vector3.Angle(Vector3.right, gunToMouse);
        Vector3 bulletAdjustedSpawn = bulletSpawn;

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

            if (flipPlayerSprite == true)
            {
                playerSpriteRenderer.flipX = false;
            }
            if (twoHanded == false)
            {
                spriteRenderer.sortingOrder = 2;
            }
        }

        else if ((int)gunToMouse.x < 0)
        {
            //gun face left
            bulletAdjustedSpawn = new Vector3(bulletSpawn.x, -bulletSpawn.y, bulletSpawn.z);
            spriteRenderer.flipX = true;
            transform.localPosition = offsetLeft;
            transform.SetPositionAndRotation(transform.position, Quaternion.FromToRotation(Vector3.left, gunToMouse));

            if (flipPlayerSprite == true)
            {
                playerSpriteRenderer.flipX = true;
            }

            if (twoHanded == false)
            {
                spriteRenderer.sortingOrder = 0;
            }
        }

        rotatedBulletSpawn = rotateAboutOrgin(bulletAdjustedSpawn, angle * Mathf.Deg2Rad) + transform.position;
        updateBulletDelay();
    }
    void spawnBullet()
    {
        float bulletAngle = angle - spreadShotgun / 2;

        for (int x = 0; x < numShot; x++)
        {
            bulletAngle += spreadShotgun / numShot;
            bulletAngle += Random.Range(-bulletSpread, bulletSpread);

            GameObject obj = Instantiate(bulletPrefab, rotatedBulletSpawn, transform.rotation);
            Vector3 direction = Quaternion.AngleAxis(bulletAngle, Vector3.forward) * Vector3.right;
            Vector3 parentVel = new Vector3(parentRbody.velocity.x, parentRbody.velocity.y, 0);
            Vector3 vel = Vector3.Normalize(direction) * (Random.Range(-speedVariation, speedVariation) + bulletSpeed) + parentVel;
            obj.GetComponent<BulletController>().setVelocity(vel);
        }
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

    Vector3 rotateAboutOrgin(Vector3 vec, float angle)
    {
        float x = vec.x*Mathf.Cos(angle) - vec.y*Mathf.Sin(angle);
        float y = vec.y*Mathf.Cos(angle) + vec.x*Mathf.Sin(angle);
        return new Vector3(x, y, 0);
    }
}