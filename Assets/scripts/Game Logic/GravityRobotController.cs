using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityRobotController : MonoBehaviour
{
    GameObject spawnManager;
    SpawnManager spawnManagerScript;
    SpriteRenderer spriteRenderer;
    public GameObject deathAnimation;
    public int health;
    public float effectRadius;
    public float force;
    public float bulletEffect;
    float currentDamageDelay;
    float damageDelay;
    bool damaged;
    // Start is called before the first frame update
    void Start()
    {
        damageDelay = 0.1f;
        spawnManager = GameObject.FindWithTag("spawnmanager");
        spawnManagerScript = spawnManager.GetComponent<SpawnManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject obj in spawnManagerScript.allDynamicSprites)
        {
            if (!spawnManagerScript.gravityRobotInstances.Contains(obj))
            {
                Rigidbody2D rbody = obj.GetComponent<Rigidbody2D>();
                Vector3 objToSelf = new Vector3(transform.position.x - obj.transform.position.x, transform.position.y - obj.transform.position.y, 0);
                if (objToSelf.magnitude < effectRadius)
                {
                    if (obj.tag == "bullet" || obj.tag == "enemybullet")
                    {
                        objToSelf = Vector3.Normalize(objToSelf) * force * bulletEffect;
                    }
                    else
                    {
                        objToSelf = Vector3.Normalize(objToSelf) * force;
                    }

                    rbody.AddForce(objToSelf, ForceMode2D.Impulse);
                }
            }

        }

        updateDamageDelay();
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "bullet")
        {
            health -= 1;
            damaged = true;
            currentDamageDelay = damageDelay;
            spriteRenderer.color = Color.red;
        }
        if (col.gameObject.tag == "lazerbeam")
        {
            health -= 5;
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
            if (health <= 0)
            {
                spawnManagerScript.allDynamicSprites.Remove(gameObject);
                Destroy(gameObject);
                Instantiate(deathAnimation, transform.position, transform.rotation);
            }
        }

        currentDamageDelay -= Time.deltaTime;
    }
}
