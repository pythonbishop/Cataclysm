using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadBarController : MonoBehaviour
{
    // Start is called before the first frame update
    GunController gunController;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (gunController.reloading)
        {
            transform.localScale = new Vector3(5 - 5 * (gunController.reloadDelay - gunController.currentReloadDelay)/gunController.reloadDelay, 0.25f, 0);
        }
        else
        {
            transform.localScale = Vector3.zero;
        }
    }

    public void updateGunReference(GunController gun)
    {
        gunController = gun;
    }
}
