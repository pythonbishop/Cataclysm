using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    // Start is called before the first frame update
    PlayerController playerController;
    DamageController damageController;
    GunController gunController;
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        damageController = GetComponent<DamageController>();
    }

    // Update is called once per frame
    void Update()
    {
        gunController = GetComponentInChildren<GunController>();
        if (Input.GetKeyDown("c"))
        {
            playerController.cycleWeapons();
            damageController.updateSpriteRenderers();
        }

        if (Input.GetKeyDown("r"))
        {
            playerController.reload();
        }
        if (Input.GetMouseButtonDown(0))
        {
            gunController.mouseDown();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            gunController.mouseUp();
        }
    }
}
