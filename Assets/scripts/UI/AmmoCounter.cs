using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoCounter : MonoBehaviour
{
    GameObject player;
    GunController playerGunController;
    Text ammoText;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        ammoText = GetComponent<Text>();
    }

    void Update()
    {
        playerGunController = player.GetComponentInChildren<GunController>();
        ammoText.text = playerGunController.currentAmmo.ToString();
    }
}
