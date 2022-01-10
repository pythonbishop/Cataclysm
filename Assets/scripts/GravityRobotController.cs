using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityRobotController : MonoBehaviour
{
    SpawnManager spawnManager;
    SpriteRenderer spriteRenderer;
    public float effectRadius;
    public float force;
    public float holdRadius;
    public float bulletEffect;
    // Start is called before the first frame update
    void Start()
    {
        spawnManager = GameObject.FindWithTag("spawnmanager").GetComponent<SpawnManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject obj in spawnManager.allDynamicSprites)
        {
            Rigidbody2D rbody = obj.GetComponent<Rigidbody2D>();
            Vector3 objToSelf = new Vector3(transform.position.x - obj.transform.position.x, transform.position.y - obj.transform.position.y, 0);
            if (objToSelf.magnitude < effectRadius && obj.tag != "enemy")
            {
                if (objToSelf.magnitude < holdRadius)
                {
                    objToSelf = -objToSelf;
                }

                if (obj.tag == "bullet" || obj.tag == "enemybullet")
                {
                    objToSelf = Vector3.Normalize(objToSelf) * bulletEffect;
                }
                else
                {
                    objToSelf = Vector3.Normalize(objToSelf) * force;
                }

                rbody.AddForce(objToSelf, ForceMode2D.Impulse);
            }
        }
    }

}
