using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityRobotController : MonoBehaviour
{
    GameObject spawnManager;
    SpawnManager spawnManagerScript;
    SpriteRenderer spriteRenderer;
    public float effectRadius;
    public float force;
    public float holdRadius;
    public float bulletEffect;
    // Start is called before the first frame update
    void Start()
    {
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

    private void OnCollisionEnter2D(Collision2D other) {
        Vector3 selfToOther = Vector3.Normalize(new Vector3(other.transform.position.x - transform.position.x, other.transform.position.y - transform.position.y, 0));
        //other.gameObject.GetComponent<Rigidbody2D>().AddForce(selfToOther * repelForce, ForceMode2D.Force);
    }
}
