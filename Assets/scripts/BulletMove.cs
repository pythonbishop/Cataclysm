using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 direction;
    public Vector3 initialVelocity;
    public float speed;
    public float rotateSpeed;
    Vector3 velocity;
    void Start()
    {
        velocity = (direction * speed) + initialVelocity;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(velocity * Time.deltaTime, Space.World);
        transform.Rotate(0, 0, rotateSpeed);
    }
}
