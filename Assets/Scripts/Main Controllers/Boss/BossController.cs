using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bullet;
    public GameObject knockback;
    public GameObject player;
    public int phase;
    Animator animator;
    Rigidbody2D rbody;
    float currentAttack;
    Vector3 selfToPlayer;
    public float attackCoolDown;
    float currentAttackCoolDown;
    public float enemySpawnCooldown;
    float currentEnemySpawnCooldown;
    public float knockbackCooldown;
    float currentKnockbackCooldown;
    float angle;
    public float startDelay;
    SpawnManager spawnManager;
    DamageController damageController;
    bool shotgunActive;
    float shotgunDelay;
    float currentShotgunDelay;
    float numShotgunToFire = 3;
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        damageController = GetComponent<DamageController>();
        spawnManager = GameObject.FindWithTag("spawnmanager").GetComponent<SpawnManager>();
        currentAttack = -1;
        shotgunDelay = 1.5f;

        currentShotgunDelay = shotgunDelay;
        currentAttackCoolDown = startDelay;
        currentEnemySpawnCooldown = startDelay;
        currentKnockbackCooldown = startDelay;
    }
    // Update is called once per frame
    void Update()
    {
        selfToPlayer = new Vector3(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y, 0);
        angle = Vector3.Angle(Vector3.right, selfToPlayer);
        float repelBullets = Random.value;

        if (damageController.health < 30 && phase != 2)
        {
            phase = 2;
            attackCoolDown = 1;
            enemySpawnCooldown = 3;
        }

        if (selfToPlayer.y < 0)
        {
            angle = -angle;
        }

        if (selfToPlayer.magnitude < 20 && currentKnockbackCooldown < 0)
        {
            knockbackAttack();
            currentKnockbackCooldown = knockbackCooldown;
        }

        if (currentAttackCoolDown < 0)
        {
            currentAttack = Random.value;
            if (!shotgunActive)
            {
                if (phase == 1)
                {
                    setShotgunActive();
                }

                else if (phase == 2)
                {
                    bullet360Attack();
                }   
            }

            currentAttackCoolDown = attackCoolDown;
        }

        if (currentEnemySpawnCooldown < 0)
        {
            currentEnemySpawnCooldown = enemySpawnCooldown;
            spawnEnemyRobot();
        }

        if (startDelay < 0 && GetComponent<FollowPlayer>().enabled == false)
        {
            GetComponent<FollowPlayer>().enabled = true;
        }

        if (shotgunActive)
        {
            if (currentShotgunDelay < 0)
            {
                currentShotgunDelay = shotgunDelay;
                shotGunAttack();
                numShotgunToFire -= 1;
            }
            if (numShotgunToFire <= 0)
            {
                shotgunActive = false;
                numShotgunToFire = 3;
            }
            currentShotgunDelay -= Time.deltaTime;
        }
        else
        {
            currentAttackCoolDown -= Time.deltaTime;
        }

        currentEnemySpawnCooldown -= Time.deltaTime;
        startDelay -= Time.deltaTime;
        currentKnockbackCooldown -= Time.deltaTime;
    }
    void knockbackAttack()
    {
        Instantiate(knockback, transform.position, transform.rotation);
    }
    void bullet360Attack()
    {
        float bulletAngle = Random.Range(-5f, 5f);
        int numShot = 60;
        for (int i = 0; i < numShot; i ++)
        {
            bulletAngle += 360/numShot;

            GameObject obj = Instantiate(bullet, transform.position, transform.rotation);
            Vector3 direction = Quaternion.AngleAxis(bulletAngle, Vector3.forward) * Vector3.right;
            Vector3 parentVel = new Vector3(rbody.velocity.x, rbody.velocity.y, 0);
            Vector3 vel = Vector3.Normalize(direction) * 20 + parentVel;

            obj.GetComponent<BulletController>().setVelocity(vel);
        }
    }
    void setShotgunActive()
    {
        shotgunActive = true;
    }
    void shotGunAttack()
    {
        Vector3 playerVelocity = player.GetComponent<Rigidbody2D>().velocity * 1.5f;
        selfToPlayer = new Vector3(player.transform.position.x + playerVelocity.x - transform.position.x, player.transform.position.y + playerVelocity.y - transform.position.y, 0);
        angle = Vector3.Angle(Vector3.right, selfToPlayer);

        if (selfToPlayer.y < 0)
        {
            angle = -angle;
        }

        float bulletAngle = angle - 50 / 2;
        float numShot = 25;

        for (float x = 0; x < numShot; x++)
        {
            bulletAngle += 50 / numShot;

            GameObject obj = Instantiate(bullet, transform.position, transform.rotation);
            Vector3 direction = Quaternion.AngleAxis(bulletAngle, Vector3.forward) * Vector3.right;
            Vector3 parentVel = new Vector3(rbody.velocity.x, rbody.velocity.y, 0);
            Vector3 vel = Vector3.Normalize(direction) * 20 + parentVel;

            obj.GetComponent<BulletController>().setVelocity(vel);
        }
    }

    void spawnEnemyRobot()
    {
        Vector3 directionFromPlayer = Vector3.Normalize(new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0));
        Vector3 position = player.transform.position + (directionFromPlayer * Random.Range(20, 50));

        while (position.x < -10 || position.x > 130 || position.y < -40 || position.y > 40)
        {
            Debug.Log("boss looping");
            directionFromPlayer = Vector3.Normalize(new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0));
            position = player.transform.position + (directionFromPlayer * Random.Range(20, 50));
        }
        if (Random.value < 0.5) spawnManager.addEnemyRobot(position);
        else spawnManager.addGravityRobot(position);
    }
}