using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour
{
    // Start is called before the first frame update
    float currentDamageDelay;
    float damageDelay;
    bool damaged;
    SpawnManager spawnManager;
    SpriteRenderer[] spriteRenderers;
    public GameObject deathAnimation;
    public int health;
    public bool isAwake;
    void Start()
    {
        damageDelay = 0.1f;
        damaged = false;
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        spawnManager = GameObject.FindWithTag("spawnmanager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (damaged & currentDamageDelay <= 0 && isAwake)
        {
            foreach (SpriteRenderer sp in spriteRenderers)
            {
                if (sp)
                {
                    sp.color = Color.white;
                }
            }
            damaged = false;
            if (health <= 0)
            {
                Destroy(gameObject);
                Instantiate(deathAnimation, transform.position, transform.rotation);
            }
        }
        currentDamageDelay -= Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (isAwake)
        {
            DamageController otherDamageController = other.gameObject.GetComponent<DamageController>();

            if (other.gameObject.tag == "bullet" || other.gameObject.tag == "enemybullet")
            {
                int bulletDamage = other.gameObject.GetComponent<BulletController>().damage;
                hurt(bulletDamage);
            }
        }
    }
    public void hurt(int damage)
    {
        damaged = true;
        health -= damage;
        currentDamageDelay = damageDelay;
        foreach (SpriteRenderer sp in spriteRenderers)
        {
            if (sp)
            {
                sp.color = Color.red;
            }
        }
    }
    public void updateSpriteRenderers()
    {
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
    }
    public void wake()
    {
        isAwake = true;
    }

    public void sleep()
    {
        isAwake = false;
    }
}
