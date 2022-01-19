using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    GameObject boss;
    DamageController bossDamageController;
    RectTransform rectTransform;
    float maxHealth;
    float maxLength;
    // Start is called before the first frame update
    void Start()
    {
        boss = GameObject.FindWithTag("boss");
        bossDamageController = boss.GetComponent<DamageController>();
        rectTransform = GetComponent<RectTransform>();
        maxHealth = bossDamageController.health;
        maxLength = rectTransform.rect.xMax;
    }

    // Update is called once per frame
    void Update()
    {
        rectTransform.sizeDelta = new Vector3(maxLength * (bossDamageController.health/maxHealth), rectTransform.sizeDelta.y, 0);
    }
}
