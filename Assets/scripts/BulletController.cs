using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 direction;
    public Vector3 initialVelocity;
    public float speed;
    public float rotateSpeed;
    public float lifetime;
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
        lifetime -= Time.deltaTime;

        if (lifetime < 0) Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (gameObject.tag == "bullet") {
            if (col.gameObject.tag != "bullet" & col.gameObject.tag != "Player") {
                Destroy(gameObject);
            }
        }

        if (gameObject.tag == "enemybullet") {
            if (col.gameObject.tag != "enemybullet") {
                Destroy(gameObject);
            }
        }
    }
}
