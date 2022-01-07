using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject destroyAnimation;
    public float lifetime;
    public int damage;
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
            Destroy(gameObject);
            spawnManagerScript.allDynamicSprites.Remove(gameObject);
            Instantiate(destroyAnimation, transform.position, transform.rotation);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        spawnManagerScript.allDynamicSprites.Remove(gameObject);
        Destroy(gameObject);
        Instantiate(destroyAnimation, transform.position, transform.rotation);
    }

    public void setVelocity(Vector3 v)
    {
        rbody = GetComponent<Rigidbody2D>();
        rbody.velocity = v;
    }

}
