using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public float verticalIn;
    public float horizontalIn;
    public float jetpackForce;
    public float drag;
    public Vector3 velocity;
    SpriteRenderer selfSprite;
    Rigidbody2D selfRigidbody;
    Collider2D selfCollider;
    void Start()
    {
        selfSprite = GetComponent<SpriteRenderer>();
        selfRigidbody = GetComponent<Rigidbody2D>();
        selfCollider = GetComponent<Collider2D>();
        selfRigidbody.drag = drag;
    }

    // Update is called once per frame
    void Update()
    {
        verticalIn = Input.GetAxis("Vertical");
        horizontalIn = Input.GetAxis("Horizontal");
        velocity = selfRigidbody.velocity;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "lazerbeam")
        {
            Physics2D.IgnoreCollision(other.collider, selfCollider);
            selfRigidbody.velocity = velocity;
        }
    }

    void FixedUpdate()
    {
        selfRigidbody.AddForce((Vector3.right * horizontalIn + Vector3.up * verticalIn) * jetpackForce);
    }
}
