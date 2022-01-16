using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bulletPrefab;
    public Vector3 bulletSpawn; // relative to gun
    public Vector3 offsetRight; // for sprite
    public Vector3 offsetLeft; // for sprite
    public float speedVariation; // amount of random variation in bullet speed
    public float bulletDelay; // delay between firing shots
    public float shotgunSpread; // shotgun spread
    public float individualBulletSpread; // amount of random variation in bullet firing angle
    public float bulletSpeed;
    public float ammo;
    public float reloadDelay;
    public float currentAmmo;
    public int numShot; // number of bullets fire per click
    public bool reloading;
    public bool twoHanded; //determines if gun is drawn on top or behind of player sprite when facing left
    public bool flipPlayerSprite; // flip player sprite to face direction gun is aiming
    public bool autoReload;
    public bool hasFireAnimation;
    public AudioClip shootingSound;
    public AudioClip emptySound;
    public AudioClip reloadSound;
    Vector3 gunToMouse;
    Vector3 mouseWorldPos;
    Vector3 rotatedBulletSpawn;
    SpriteRenderer spriteRenderer;
    SpriteRenderer playerSpriteRenderer;
    Rigidbody2D parentRbody;
    Animator animatorController;
    float angle;
    float currentDelay;
    float currentReloadDelay;
    bool mousePress;
    bool empty;
    AudioSource audioSource;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerSpriteRenderer = gameObject.transform.parent.GetComponent<SpriteRenderer>();
        parentRbody = transform.parent.gameObject.GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

        spriteRenderer.sortingLayerName = "character";
        gunToMouse = new Vector3();
        currentDelay = 0;
        currentReloadDelay = reloadDelay;
        reloading = true;

        if (hasFireAnimation)
        {
            animatorController = GetComponent<Animator>();
        }
    }
    // Update is called once per frame
    void Update()
    {
        // calculate bullet spawn position and direction
        mouseWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition[0], Input.mousePosition[1]));
        gunToMouse.Set(mouseWorldPos.x - transform.position[0], mouseWorldPos.y - transform.position[1], 0);
        angle = Vector3.Angle(Vector3.right, gunToMouse);
        Vector3 bulletAdjustedSpawn = bulletSpawn;

        // adjust angle for flipping sprite
        if (gunToMouse.y < 0)
        {
            angle = -angle;
        }

        // update gun position and direction for facing right
        if ((int)gunToMouse.x > 0)
        {
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

        // update gun position and direction for facing left
        else if ((int)gunToMouse.x < 0)
        {
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

        // adjust bullet direction for gun facing direction
        rotatedBulletSpawn = rotateAboutOrgin(bulletAdjustedSpawn, angle * Mathf.Deg2Rad) + transform.position;

        // shooting logic
        if (currentDelay <= 0 & mousePress & !reloading)
        {
            if (!empty)
            {
                currentAmmo -= 1;
                spawnBullet();
            }
            else
            {
                audioSource.PlayOneShot(emptySound);
            }
            currentDelay = bulletDelay;
        }

        //set gun to empty and autoreload if possible
        if (currentAmmo <= 0)
        {
            empty = true;
            if (autoReload)
            {
                reloading = true;
                audioSource.PlayOneShot(reloadSound);
            }
        }

        // reloading logic
        if (reloading)
        {
            if (currentReloadDelay < 0)
            {
                reloading = false;
                empty = false;
                currentReloadDelay = reloadDelay;
                currentAmmo = ammo;
                currentDelay = 0;
            }
            currentReloadDelay -= Time.deltaTime;
        }

        currentDelay -= Time.deltaTime;
    }
    void spawnBullet()
    {
        //update animator parameters
        if (hasFireAnimation)
        {
            animatorController.SetTrigger("Fire");
        }

        float bulletAngle = angle - shotgunSpread / 2;
        audioSource.PlayOneShot(shootingSound);

        for (int x = 0; x < numShot; x++)
        {
            bulletAngle += shotgunSpread / numShot;
            bulletAngle += Random.Range(-individualBulletSpread, individualBulletSpread);

            GameObject obj = Instantiate(bulletPrefab, rotatedBulletSpawn, transform.rotation);
            Vector3 direction = Quaternion.AngleAxis(bulletAngle, Vector3.forward) * Vector3.right;
            Vector3 parentVel = new Vector3(parentRbody.velocity.x, parentRbody.velocity.y, 0);
            Vector3 vel = Vector3.Normalize(direction) * (Random.Range(-speedVariation, speedVariation) + bulletSpeed) + parentVel;


            if (obj.TryGetComponent<BulletController>(out BulletController a))
            {
                obj.GetComponent<BulletController>().setVelocity(vel);
            }
            else if (obj.TryGetComponent<RocketController>(out RocketController z))
            {
                obj.GetComponent<RocketController>().setVelocity(vel);
            }
        }
    }

    Vector3 rotateAboutOrgin(Vector3 vec, float angle)
    {
        // rotate vector about orgin (radians)
        float x = vec.x*Mathf.Cos(angle) - vec.y*Mathf.Sin(angle);
        float y = vec.y*Mathf.Cos(angle) + vec.x*Mathf.Sin(angle);
        return new Vector3(x, y, 0);
    }

    public void mouseDown()
    {
        mousePress = true;
    }

    public void mouseUp()
    {
        mousePress = false;
    }
}