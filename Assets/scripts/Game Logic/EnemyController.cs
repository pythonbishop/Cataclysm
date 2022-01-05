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
    public GameObject deathAnimation;
    public float openFireDistance;
    public int helath;
    Vector3 rotatedBulletSpawn;
    SpriteRenderer gunSpriteRenderer;
    SpriteRenderer spriteRenderer;
    Transform gunTransform;
    GameObject player;
    Vector3 gunToPlayer;
    GameObject spawnManagerObject;
    SpawnManager spawnManager;
    float angle;
    float currentDelay;
    float currentDamageDelay;
    float damageDelay;
    bool damaged;
    void Start()
    {
        gunTransform = transform.GetChild(0);
        gunSpriteRenderer = gunTransform.gameObject.GetComponent<SpriteRenderer>();
        gunToPlayer = new Vector3();
        player = GameObject.FindGameObjectWithTag("Player");
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        damageDelay = 0.1f;
        spawnManagerObject = GameObject.FindWithTag("spawnmanager");
        spawnManager = spawnManagerObject.GetComponent<SpawnManager>();
    }
    // Update is called once per frame
    void Update()
    {
        gunToPlayer.Set(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y, 0); //vector from self to player (gun direction)
        angle = Vector3.Angle(Vector3.right, gunToPlayer); //gun direction angle in degrees

        rotatedBulletSpawn = Vector3.RotateTowards(bulletSpawn, gunToPlayer, Mathf.Deg2Rad * angle, bulletSpawn.magnitude);
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

            if (flipSprite == true)
            {
                spriteRenderer.flipX = false;
            }
            if (twoHanded == false)
            {
                gunSpriteRenderer.sortingOrder = 2;
            }
        }

        else if ((int)gunToPlayer.x < 0)
        {
            //gun face left
            gunSpriteRenderer.flipX = true;
            gunTransform.localPosition = offsetLeft;
            gunTransform.SetPositionAndRotation(gunTransform.position, Quaternion.FromToRotation(Vector3.left, gunToPlayer));

            if (flipSprite == true)
            {
                spriteRenderer.flipX = true;
            }

            if (twoHanded == false)
            {
                gunSpriteRenderer.sortingOrder = 0;
            }
        }

        if (gunToPlayer.magnitude < openFireDistance)
        {
            shooting = true;
        }

        else
        {
            shooting = false;
        }

        updateBulletDelay();
        updateDamageDelay();
    }
    void spawnBullet()
    {
        GameObject obj = Instantiate(bulletPrefab, rotatedBulletSpawn, new Quaternion());
        float bulletAngle = angle + Random.Range(-2, 2);
        float speed = obj.GetComponent<BulletController>().speed;
        Vector3 direction = Quaternion.AngleAxis(bulletAngle, Vector3.forward) * Vector3.right;

        obj.GetComponent<Rigidbody2D>().velocity = speed * direction;
    }

    void updateBulletDelay()
    {
        if (currentDelay <= 0 & shooting)
        {
            currentDelay = bulletDelay + Random.Range(-0.2f, 0.2f);
            spawnBullet();
        }

        currentDelay -= Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "bullet")
        {
            helath -= 1;
            damaged = true;
            currentDamageDelay = damageDelay;
            spriteRenderer.color = Color.red;
        }
        if (col.gameObject.tag == "lazerbeam")
        {
            helath -= 3;
            damaged = true;
            currentDamageDelay = damageDelay;
            spriteRenderer.color = Color.red;
        }
    }

    void updateDamageDelay()
    {
        if (damaged & currentDamageDelay <= 0)
        {
            spriteRenderer.color = Color.white;
            if (helath <= 0)
            {
                spawnManager.allDynamicSprites.Remove(gameObject);
                Destroy(gameObject);
                Instantiate(deathAnimation, transform.position, transform.rotation);
            }
        }

        currentDamageDelay -= Time.deltaTime;
    }
}