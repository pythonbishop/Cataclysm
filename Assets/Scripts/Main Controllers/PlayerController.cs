using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Start is called before the first frame update
    public GameObject[] guns;
    public List<bool> activeGuns;
    public List<float> ammo;
    public bool autoReload;
    GameObject currentGun;
    SpriteRenderer selfSprite;
    Collider2D selfCollider;
    SpawnManager spawnManager;
    GunController currentGunController;
    int gunIndex;
    void Start()
    {
        spawnManager = GameObject.FindWithTag("spawnmanager").GetComponent<SpawnManager>();
        selfSprite = GetComponent<SpriteRenderer>();
        selfCollider = GetComponent<Collider2D>();
        gunIndex = 0;
        currentGun = transform.GetChild(0).gameObject;
        currentGunController = currentGun.GetComponent<GunController>();
        currentGunController.autoReload = autoReload;
        GetComponentInChildren<ReloadBarController>().updateGunReference(currentGun.GetComponent<GunController>());
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void reload()
    {
        if (!currentGun.GetComponent<GunController>().empty)
        {
            currentGun.GetComponent<GunController>().reloading = true;
            currentGun.GetComponent<AudioSource>().PlayOneShot(currentGun.GetComponent<GunController>().reloadSound);
        }
    }

    public void cycleWeapons()
    {
        ammo[gunIndex] = currentGunController.currentAmmo;
        gunIndex += 1;
        if (gunIndex > guns.Length - 1)
        {
            gunIndex = 0;
        }

        while (!activeGuns[gunIndex])
        {
            gunIndex += 1;
            if (gunIndex > guns.Length - 1)
            {
                gunIndex = 0;
            }
        }
        
        Destroy(currentGun);
        currentGun = Instantiate(guns[gunIndex], transform.position, transform.rotation, transform);
        currentGunController = currentGun.GetComponent<GunController>();

        currentGunController.autoReload = autoReload;
        currentGunController.currentAmmo = ammo[gunIndex];
        GetComponentInChildren<ReloadBarController>().updateGunReference(currentGun.GetComponent<GunController>());
    }

    public void switchTo(int index)
    {
        if (index > -1 && index < guns.Length && activeGuns[index])
        {
            ammo[gunIndex] = currentGunController.currentAmmo;
            gunIndex = index;
            
            Destroy(currentGun);
            currentGun = Instantiate(guns[gunIndex], transform.position, transform.rotation, transform);
            currentGunController = currentGun.GetComponent<GunController>();

            currentGunController.autoReload = autoReload;
            currentGunController.currentAmmo = ammo[gunIndex];
        }
    }
}