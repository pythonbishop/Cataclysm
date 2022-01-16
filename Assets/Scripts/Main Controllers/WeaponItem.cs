using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItem : MonoBehaviour
{
    // Start is called before the first frame update
    public int weaponToGive;
    public GameObject deathAnimation;
    void Start()
    {
        
    }
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other) {
        other.gameObject.GetComponent<PlayerController>().activeGuns[weaponToGive] = true;
        Destroy(gameObject);
        Instantiate(deathAnimation, transform.position, transform.rotation);
    }
}
