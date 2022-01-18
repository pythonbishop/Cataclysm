using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemOnDeath : MonoBehaviour
{
    // Start is called before the first frame update
    public bool random;
    public GameObject item;

    public GameObject[] allItems;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy() {
        if (random)
        {
            int randInt = Random.Range(0, allItems.Length*20);
            
            if (randInt < allItems.Length)
            {
                Instantiate(allItems[randInt], transform.position, transform.rotation);
            }
        }

        else if (item)
        {
            Instantiate(item, transform.position, transform.rotation);
        }
    }
}
