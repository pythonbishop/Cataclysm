using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject player;
    Vector3 initialPosition;
    public float speed;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = new Vector3();

        position = -player.transform.position * speed / 100 + player.transform.position + initialPosition;

        transform.position = position;
    }
}
