using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public float verticalIn;
    public float horizontalIn;
    public GameObject[] guns;
    GameObject currentGun;
    public float jetpackForce;
    SpriteRenderer selfSprite;
    Rigidbody2D selfRigidbody;
    Collider2D selfCollider;
    int gunIndex;
    void Start()
    {
        selfSprite = GetComponent<SpriteRenderer>();
        selfRigidbody = GetComponent<Rigidbody2D>();
        selfCollider = GetComponent<Collider2D>();
        gunIndex = 0;
        currentGun = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        currentGun = transform.GetChild(0).gameObject;
        verticalIn = Input.GetAxis("Vertical");
        horizontalIn = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown("c"))
        {
            gunIndex += 1;
            if (gunIndex > guns.Length - 1)
            {
                gunIndex = 0;
            }
            Destroy(currentGun);
            Instantiate(guns[gunIndex], transform.position, transform.rotation, transform);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {

    }

    void FixedUpdate()
    {
        selfRigidbody.AddForce((Vector3.right * horizontalIn + Vector3.up * verticalIn) * jetpackForce);
    }
}
