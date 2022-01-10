using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidSelf : MonoBehaviour
{
    // attached to enemy gameobjects to space them apart and prevent them from colliding with each other
    public float distSqr; // maximum distance where force is applied
    public float force; // repel force
    Rigidbody2D rbody;
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(gameObject.tag);
        foreach (GameObject obj in objects)
        {
            Vector3 objToSelf = new Vector3(transform.position.x - obj.transform.position.x, transform.position.y - obj.transform.position.y, 0);
            if (objToSelf.sqrMagnitude < distSqr)
            {
                objToSelf = Vector3.Normalize(objToSelf) * force;
                rbody.AddForce(objToSelf, ForceMode2D.Impulse);
            }

        }
    }
}
