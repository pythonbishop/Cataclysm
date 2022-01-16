using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentBulletController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject destroyAnimation;
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

    // Update is called once per frame
    void Update()
    {
        lifetime -= Time.deltaTime;

        if (lifetime < 0)
        // || velocity.sqrMagnitude < 4
        {
            Destroy(gameObject);
            Instantiate(destroyAnimation, transform.position, transform.rotation);
        }
    }
    public void setVelocity(Vector3 v)
    {
        rbody = GetComponent<Rigidbody2D>();
        rbody.velocity = v;
    }


}
