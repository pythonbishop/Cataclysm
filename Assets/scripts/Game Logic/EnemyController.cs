using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 velocity;
    public Vector3 bulletSpawn;
    public Vector3 offsetRight;
    public Vector3 offsetLeft;
    public GameObject bulletPrefab;
    public float bulletDelay;
    public bool flipSprite;
    public bool twoHanded;
    public bool shooting;
    
    Vector3 rotatedBulletSpawn;
    SpriteRenderer gunSpriteRenderer;
    Transform gunTransform;
    GameObject player;
    SpriteRenderer spriteRenderer;
    Vector3 gunToPlayer;
    float angle;
    float currentDelay;
    void Start()
    {
        gunTransform = transform.GetChild(0);
        gunSpriteRenderer = gunTransform.gameObject.GetComponent<SpriteRenderer>();
        gunToPlayer = new Vector3();
        player = GameObject.FindGameObjectWithTag("Player");
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        gunToPlayer.Set(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y, 0);
        angle = Vector3.Angle(Vector3.right, gunToPlayer);

        rotatedBulletSpawn = Vector3.RotateTowards(bulletSpawn, gunToPlayer, angle, bulletSpawn.magnitude);
        rotatedBulletSpawn = Vector3.ClampMagnitude(rotatedBulletSpawn, bulletSpawn.magnitude) + gunTransform.position;

        if (gunToPlayer.y < 0)
        {
            angle = -angle;
        } 
        
        if ((int)gunToPlayer.x > 0)
        {
            //gun face right
            gunSpriteRenderer.flipX = false;
            gunTransform.localPosition = offsetRight;
            gunTransform.SetPositionAndRotation(gunTransform.position, Quaternion.FromToRotation(Vector3.right, gunToPlayer));

            if (flipSprite == true) {
                spriteRenderer.flipX = false;
                Debug.Log("false");
            }
            if (twoHanded == false) {
                gunSpriteRenderer.sortingOrder = 2;
            }
        }

        else if ((int)gunToPlayer.x < 0)
        {
            //gun face left
            gunSpriteRenderer.flipX = true;
            gunTransform.localPosition = offsetLeft;
            gunTransform.SetPositionAndRotation(gunTransform.position, Quaternion.FromToRotation(Vector3.left, gunToPlayer));

            if (flipSprite == true) {
                Debug.Log("true");
                spriteRenderer.flipX = true;
            }

            if (twoHanded == false) {
                gunSpriteRenderer.sortingOrder = 0;
            }
        }

        updateBulletDelay();
    }
    void spawnBullet()
    {
        GameObject obj = Instantiate(bulletPrefab, rotatedBulletSpawn, new Quaternion());
        float bulletAngle = angle + Random.Range(-2, 2);
        float speed = obj.GetComponent<BulletController>().speed;
        Vector3 direction = Quaternion.AngleAxis(bulletAngle, Vector3.forward) * Vector3.right;

        obj.GetComponent<Rigidbody2D>().AddForce(direction * speed, ForceMode2D.Impulse);
    }

    void updateBulletDelay()
    {
        if (currentDelay <= 0 & shooting)
        {
            currentDelay = bulletDelay;
            spawnBullet();
        }
        
        currentDelay -= Time.deltaTime;
    }
}