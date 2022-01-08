using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthCounter : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject player;
    DamageController playerDamageController;
    Text healthText;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerDamageController = player.GetComponent<DamageController>();
        healthText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = playerDamageController.health.ToString();
    }
}
