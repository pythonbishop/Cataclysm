using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    // Start is called before the first frame update
    public float verticalIn;
    public float horizontalIn;
    public float jetpackForce;
    Rigidbody2D selfRigidbody;
    PlayerController playerController;
    DamageController damageController;
    GunController gunController;
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        damageController = GetComponent<DamageController>();
        selfRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        gunController = GetComponentInChildren<GunController>();
        verticalIn = Input.GetAxis("Vertical");
        horizontalIn = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown("c"))
        {
            playerController.cycleWeapons();
            damageController.updateSpriteRenderers();
        }

        if (Input.GetKeyDown("r"))
        {
            playerController.reload();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            playerController.switchTo(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            playerController.switchTo(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            playerController.switchTo(2);
        }

        if (Input.GetKey(KeyCode.Mouse0))
        {
            gunController.mouseDown();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            gunController.mouseUp();
        }
    }
    void FixedUpdate()
    {
        selfRigidbody.AddForce((Vector3.right * horizontalIn + Vector3.up * verticalIn) * jetpackForce);
        //transform.Translate((Vector3.right * horizontalIn + Vector3.up * verticalIn) * jetpackForce * Time.deltaTime);
    }
}
