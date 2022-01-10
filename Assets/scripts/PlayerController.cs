using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Start is called before the first frame update
    public float verticalIn;
    public float horizontalIn;
    public bool[] isLoaded;
    public GameObject[] guns;
    public float jetpackForce;
    public bool autoReload;
    GameObject currentGun;
    SpriteRenderer selfSprite;
    Rigidbody2D selfRigidbody;
    Collider2D selfCollider;
    SpawnManager spawnManager;
    int gunIndex;
    void Start()
    {
        spawnManager = GameObject.FindWithTag("spawnmanager").GetComponent<SpawnManager>();
        selfSprite = GetComponent<SpriteRenderer>();
        selfRigidbody = GetComponent<Rigidbody2D>();
        selfCollider = GetComponent<Collider2D>();
        gunIndex = 0;
        currentGun = transform.GetChild(0).gameObject;
        currentGun.GetComponent<GunController>().autoReload = autoReload;
    }

    // Update is called once per frame
    void Update()
    {
        verticalIn = Input.GetAxis("Vertical");
        horizontalIn = Input.GetAxis("Horizontal");
    }

    public void reload()
    {
        currentGun.GetComponent<GunController>().reloading = true;
    }

    void FixedUpdate()
    {
        selfRigidbody.AddForce((Vector3.right * horizontalIn + Vector3.up * verticalIn) * jetpackForce);
    }

    public void cycleWeapons()
    {
        gunIndex += 1;
        if (gunIndex > guns.Length - 1)
        {
            gunIndex = 0;
        }
        Destroy(currentGun);
        currentGun = Instantiate(guns[gunIndex], transform.position, transform.rotation, transform);
        currentGun.GetComponent<GunController>().autoReload = autoReload;
    }

}