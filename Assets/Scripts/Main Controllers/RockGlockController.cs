using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockGlockController : MonoBehaviour
{
    // Start is called before the first frame update
    public float bulletSpeed;
    public Vector3 bulletSpawn;
    public Vector3 offsetRight;
    public Vector3 offsetLeft;
    public GameObject bulletPrefab;
    public float bulletDelay;
    public bool flipSprite;
    public bool twoHanded;
    public bool shooting;
    public float openFireDistance;
    Vector3 rotatedBulletSpawn;
    SpriteRenderer gunSpriteRenderer;
    SpriteRenderer spriteRenderer;
    GameObject player;
    Vector3 gunToPlayer;
    Rigidbody2D rbody;
    float angle;
    float currentDelay;
    Animator animatorController;
    public bool isAwake;
    public AudioClip shootingSound;
    AudioSource audioSource;
    void Start()
    {
        gunSpriteRenderer = GetComponent<SpriteRenderer>();
        animatorController = GetComponent<Animator>();
        spriteRenderer = transform.parent.gameObject.GetComponent<SpriteRenderer>();
        rbody = transform.parent.gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        gunToPlayer = new Vector3();
    }
    // Update is called once per frame
    void Update()
    {
        if (isAwake)
        {
            gunToPlayer.Set(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y, 0); //vector from self to player (gun direction)
            angle = Vector3.Angle(Vector3.right, gunToPlayer); //gun direction angle in degrees
            Vector3 bulletAdjustedSpawn = bulletSpawn;

            if (gunToPlayer.y < 0)
            {
                angle = -angle;
            }


            if ((int)gunToPlayer.x > 0)
            {
                //gun face right
                gunSpriteRenderer.flipX = false;
                transform.localPosition = offsetRight;
                transform.SetPositionAndRotation(transform.position, Quaternion.FromToRotation(Vector3.right, gunToPlayer));

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
                bulletAdjustedSpawn = new Vector3(bulletSpawn.x, -bulletSpawn.y, bulletSpawn.z);
                gunSpriteRenderer.flipX = true;
                transform.localPosition = offsetLeft;
                transform.SetPositionAndRotation(transform.position, Quaternion.FromToRotation(Vector3.left, gunToPlayer));

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

            rotatedBulletSpawn = rotateAboutOrgin(bulletSpawn, angle * Mathf.Deg2Rad) + transform.position;
            updateBulletDelay();
        }
    }
    void spawnBullet()
    {
        animatorController.SetTrigger("Fire");
        angle += Random.Range(-6, 6);
        GameObject obj = Instantiate(bulletPrefab, rotatedBulletSpawn, new Quaternion());

        Vector3 direction = Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.right;
        Vector3 parentVel = new Vector3(rbody.velocity.x, rbody.velocity.y, 0);
        Vector3 vel = Vector3.Normalize(direction) * bulletSpeed + parentVel;
        
        obj.GetComponent<BulletController>().setVelocity(vel);
    }
    void updateBulletDelay()
    {
        if (currentDelay <= 0 & shooting)
        {
            currentDelay = bulletDelay + Random.Range(-0.2f, 0.2f);
            spawnBullet();
            audioSource.PlayOneShot(shootingSound);
        }

        currentDelay -= Time.deltaTime;
    }
    Vector3 rotateAboutOrgin(Vector3 vec, float angle)
    {
        float x = vec.x*Mathf.Cos(angle) - vec.y*Mathf.Sin(angle);
        float y = vec.y*Mathf.Cos(angle) + vec.x*Mathf.Sin(angle);
        return new Vector3(x, y, 0);
    }

    public void wake()
    {
        isAwake = true;
        gunSpriteRenderer.enabled = true;
    }

    public void sleep()
    {
        isAwake = false;
        gunSpriteRenderer.enabled = false;
    }

}
