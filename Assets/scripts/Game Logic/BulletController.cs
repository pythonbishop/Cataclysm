using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 direction;
    public GameObject destroyAnimation;
    Rigidbody2D rbody;
    public float speed;
    public float rotateSpeed;
    public float lifetime;
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, rotateSpeed);
        lifetime -= Time.deltaTime;

        if (lifetime < 0) Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (gameObject.tag == "bullet") {
            if (col.gameObject.tag != "bullet" & col.gameObject.tag != "Player") {
                Destroy(gameObject);
                Instantiate(destroyAnimation, transform.position, transform.rotation);
            }
            else {
                Physics2D.IgnoreCollision(col.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            }
        }

        if (gameObject.tag == "enemybullet") {
            if (col.gameObject.tag != "enemybullet") {
                Destroy(gameObject);
                Instantiate(destroyAnimation, transform.position, transform.rotation);
            }
            else {
                Physics2D.IgnoreCollision(col.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            }
        }
    }
}
