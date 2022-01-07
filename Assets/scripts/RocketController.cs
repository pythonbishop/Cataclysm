using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject projectiles;
    public float lifetime;
    public int numProjectiles;
    public int damage;
    public float projectileSpeed;
    GameObject SpawnManager;
    Vector3 direction;
    SpawnManager spawnManagerScript;
    Rigidbody2D rbody;
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        SpawnManager = GameObject.FindWithTag("spawnmanager");
        spawnManagerScript = SpawnManager.GetComponent<SpawnManager>();
        spawnManagerScript.allDynamicSprites.Add(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        lifetime -= Time.deltaTime;

        if (lifetime < 0)
        // || velocity.sqrMagnitude < 4
        {

            float deltaAngle = 360/numProjectiles;
            float angle = 0;
            Vector3 spawnPos = Vector3.right;

            for (int a = 0; a < numProjectiles; a++)
            {
                angle += deltaAngle;

                Destroy(gameObject);
                spawnManagerScript.allDynamicSprites.Remove(gameObject);
                
                Vector3 direction = rotateAboutOrgin(spawnPos, angle);
                GameObject obj = Instantiate(projectiles, direction + transform.position, transform.rotation);

                Vector3 vel = direction * projectileSpeed;
                obj.GetComponent<BulletController>().setVelocity(vel);
            }
        }
    }
    public void setVelocity(Vector3 v)
    {
        rbody = GetComponent<Rigidbody2D>();
        rbody.velocity = v;
    }
    Vector3 rotateAboutOrgin(Vector3 vec, float angle)
    {
        float x = vec.x*Mathf.Cos(angle) - vec.y*Mathf.Sin(angle);
        float y = vec.y*Mathf.Cos(angle) + vec.x*Mathf.Sin(angle);
        return new Vector3(x, y, 0);
    }
}

