using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceFieldRobotController : MonoBehaviour
{
    SpawnManager spawnManager;
    SpriteRenderer spriteRenderer;
    public float effectMaxRadius;
    public float effectMinRadius;
    public float force;
    // Start is called before the first frame update
    void Start()
    {
        spawnManager = GameObject.FindWithTag("spawnmanager").GetComponent<SpawnManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (GameObject obj in spawnManager.allDynamicSprites)
        {
            Rigidbody2D rbody = obj.GetComponent<Rigidbody2D>();
            Vector3 objToSelf = new Vector3(obj.transform.position.x - transform.position.x, obj.transform.position.y - transform.position.y, 0);
            if (objToSelf.magnitude < effectMaxRadius && objToSelf.magnitude > effectMinRadius && obj.tag == "bullet")
            {
                objToSelf = Vector3.Normalize(objToSelf) * force;
                rbody.AddForce(objToSelf, ForceMode2D.Impulse);
            }
        }
    }
}