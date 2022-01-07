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
    GameObject deathAnimation;
    public int health;
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
        if (damaged & currentDamageDelay <= 0)
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
                spawnManager.allDynamicSprites.Remove(gameObject);
                Destroy(gameObject);
                Instantiate(deathAnimation, transform.position, transform.rotation);
            }
        }
        currentDamageDelay -= Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {

        DamageController otherDamageController = other.gameObject.GetComponent<DamageController>();
        damaged = true;

        if (other.gameObject.tag == "bullet" || other.gameObject.tag == "enemybullet")
        {
            int bulletDamage = other.gameObject.GetComponent<BulletController>().damage;
            health -= bulletDamage;
            currentDamageDelay = damageDelay;
            foreach (SpriteRenderer sp in spriteRenderers)
            {
                if (sp)
                {
                    sp.color = Color.red;
                }
            }
        }
    }

    public void updateSpriteRenderers()
    {
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
    }
}
