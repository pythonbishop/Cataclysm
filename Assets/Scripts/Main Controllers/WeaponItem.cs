using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItem : MonoBehaviour
{
    // Start is called before the first frame update
    public int weaponToGive;
    public string popUpText;
    void Start()
    {

    }
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameObject.FindWithTag("event").GetComponent<PopUpController>().open(3, popUpText);
            other.gameObject.GetComponent<PlayerController>().activeGuns[weaponToGive] = true;
            Destroy(gameObject);
        }

    }
}
