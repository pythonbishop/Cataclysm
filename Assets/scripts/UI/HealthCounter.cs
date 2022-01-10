using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthCounter : MonoBehaviour
{
    GameObject player;
    DamageController playerDamageController;
    Text healthText;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerDamageController = player.GetComponent<DamageController>();
        healthText = GetComponent<Text>();
    }

    void Update()
    {
        healthText.text = playerDamageController.health.ToString();
    }
}
