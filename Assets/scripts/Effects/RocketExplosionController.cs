using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketExplosionController : MonoBehaviour
{
    public float effectRadius;
    public float effect;
    public float bulletEffect;
    public float sizeSpeed;
    public float lifetime;
    public int damage;
    SpawnManager spawnManager;
    Vector3 scale;
    // Start is called before the first frame update
    void Start()
    {
        scale = new Vector3(1, 1, 0);
        spawnManager = GameObject.FindWithTag("spawnmanager").GetComponent<SpawnManager>();

        foreach (GameObject obj in spawnManager.allDynamicSprites)
        {
            Rigidbody2D rbody = obj.GetComponent<Rigidbody2D>();
            Vector3 objToSelf = new Vector3(obj.transform.position.x - transform.position.x, obj.transform.position.y - transform.position.y, 0);
            if (objToSelf.magnitude < effectRadius)
            {

                if (obj.tag == "bullet" || obj.tag == "enemybullet")
                {
                    objToSelf = Vector3.Normalize(objToSelf) * bulletEffect;
                }
                else
                {
                    objToSelf = Vector3.Normalize(objToSelf) * effect;
                    obj.GetComponent<DamageController>().hurt(damage);
                }

                rbody.velocity += new Vector2(objToSelf.x, objToSelf.y);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        lifetime -= Time.deltaTime;

        if (lifetime < 0)
        {
            Destroy(gameObject);
        }

        scale.x += sizeSpeed * Time.deltaTime;
        scale.y += sizeSpeed * Time.deltaTime;
        transform.localScale = scale;
    }
}
