using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthCounter : MonoBehaviour
{
    GameObject player;
    DamageController playerDamageController;
    Image image;
    public Sprite[] sprites;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerDamageController = player.GetComponent<DamageController>();
        image = GetComponent<Image>();
    }

    void Update()
    {
        image.sprite = sprites[playerDamageController.health];
    }
}
