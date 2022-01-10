using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    // controls most projectiles
    public GameObject destroyAnimation; //gameobject to be spawned when gameObject is destroyed
    public float lifetime;
    public int damage;
    Vector3 direction;
    SpawnManager spawnManager;
    Rigidbody2D rbody;
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        spawnManager = GameObject.FindWithTag("spawnmanager").GetComponent<SpawnManager>();
    }

    void Update()
    {
        lifetime -= Time.deltaTime;

        if (lifetime < 0)
        {
            Destroy(gameObject);
            Instantiate(destroyAnimation, transform.position, transform.rotation);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Destroy(gameObject);
        Instantiate(destroyAnimation, transform.position, transform.rotation);
    }

    public void setVelocity(Vector3 v)
    {
        rbody = GetComponent<Rigidbody2D>();
        rbody.velocity = v;
    }
}
