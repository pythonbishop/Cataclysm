using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadBarController : MonoBehaviour
{
    // Start is called before the first frame update
    bool startReloading;
    float currentScale;
    float reloadingTime;
    GunController gunController;
    void Start()
    {
        updateGunReference();
    }

    // Update is called once per frame
    void Update()
    {
        if (gunController.reloading)
        {
            transform.localScale = new Vector3(5 - 5 * (gunController.reloadDelay - gunController.currentReloadDelay)/gunController.reloadDelay, 0.25f, 0);
        }
        else if (!Vector3.Equals(transform.localScale, Vector3.zero))
        {
            transform.localScale = Vector3.zero;
        }
    }

    public void updateGunReference()
    {
        gunController = transform.parent.GetComponentInChildren<GunController>();
    }
}
