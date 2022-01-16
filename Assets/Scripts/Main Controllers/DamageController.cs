using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour
{
    // manages health of gameobjects
    public GameObject deathAnimation;
    public int health;
    public bool isAwake;
    public bool disable;
    float currentDamageDelay;
    float damageDelay;
    bool damaged;
    SpriteRenderer[] spriteRenderers;
    public AudioClip deathSound;
    public AudioClip damagedSound;
    void Start()
    {
        damageDelay = 0.1f;
        damaged = false;
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
    }

    void Update()
    {
        if (damaged & currentDamageDelay <= 0 && isAwake)
        {
            // changes sprite color back to white after short delay
            foreach (SpriteRenderer sp in spriteRenderers)
            {
                if (sp)
                {
                    sp.color = Color.white;
                }
            }
            if (health <= 0)
            {
                if (disable)
                {
                    gameObject.SetActive(false);
                }
                else
                {
                    Destroy(gameObject);
                }

                GameObject destoryed = Instantiate(deathAnimation, transform.position, transform.rotation);
                destoryed.GetComponent<AudioSource>().PlayOneShot(deathSound);
            }
            damaged = false;
        }
        currentDamageDelay -= Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (isAwake)
        {
            if (other.gameObject.tag == "bullet" || other.gameObject.tag == "enemybullet")
            {
                int bulletDamage = other.gameObject.GetComponent<BulletController>().damage;
                hurt(bulletDamage);
            }
        }
    }
    public void hurt(int damage)
    {
        // lowers health and changes sprite color to red
        if (isAwake)
        {
            GetComponent<AudioSource>().PlayOneShot(damagedSound);
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
