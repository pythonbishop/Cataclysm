using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidSelf : MonoBehaviour
{
    // Start is called before the first frame update
    public float distSqr;
    public float force;
    Rigidbody2D rbody;
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(gameObject.tag);
        foreach (GameObject obj in objects) {
            Vector3 objToSelf = new Vector3(transform.position.x - obj.transform.position.x, transform.position.y - obj.transform.position.y, 0);
            if (objToSelf.sqrMagnitude < distSqr)
            {
                objToSelf = Vector3.Normalize(objToSelf) * force;
                rbody.AddForce(objToSelf, ForceMode2D.Impulse);
            }

        }
    }
}
