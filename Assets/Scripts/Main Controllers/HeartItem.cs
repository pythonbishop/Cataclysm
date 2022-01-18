using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartItem : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject deathAnimation;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other) {
        other.gameObject.GetComponent<DamageController>().health += 3;
        Destroy(gameObject);
        Instantiate(deathAnimation, transform.position, transform.rotation);
    }
}
