using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 direction;
    public GameObject destroyAnimation;
    GameObject SpawnManager;
    public Vector2 velocity;
    public float speed;
    public float rotateSpeed;
    public float lifetime;
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
        transform.Rotate(0, 0, rotateSpeed);
        lifetime -= Time.deltaTime;
        velocity = rbody.velocity;

        if (lifetime < 0)
        // || velocity.sqrMagnitude < 4
        {
            Destroy(gameObject);
            spawnManagerScript.allDynamicSprites.Remove(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (gameObject.tag == "bullet")
        {
            if (col.gameObject.tag != "bullet" & col.gameObject.tag != "Player")
            {
                spawnManagerScript.allDynamicSprites.Remove(gameObject);
                Destroy(gameObject);
                Instantiate(destroyAnimation, transform.position, transform.rotation);
            }
            else
            {
                Physics2D.IgnoreCollision(col.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
                rbody.velocity = velocity;
            }
        }

        if (gameObject.tag == "enemybullet")
        {
            if (col.gameObject.tag != "enemybullet" & col.gameObject.tag != "enemy")
            {
                spawnManagerScript.allDynamicSprites.Remove(gameObject);
                Destroy(gameObject);
                Instantiate(destroyAnimation, transform.position, transform.rotation);
            }
            else
            {
                Physics2D.IgnoreCollision(col.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
                rbody.velocity = velocity;
            }
        }
    }
}
