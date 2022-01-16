using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldEffectSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject forceField;
    public float interval;
    float currentTime;
    void Start()
    {
        currentTime = interval;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= interval * Time.deltaTime;
        if (currentTime < 0)
        {
            Instantiate(forceField, transform.position, transform.rotation);
            currentTime = interval;
        }
    }
}
