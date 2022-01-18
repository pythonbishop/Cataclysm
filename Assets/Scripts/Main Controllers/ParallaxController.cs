using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject mainCamera;
    Vector3 initialPosition;
    public float speed;
    void Start()
    {
        mainCamera = GameObject.FindWithTag("MainCamera");
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 position = new Vector3();

        position = -mainCamera.transform.position * speed / 100 + mainCamera.transform.position + initialPosition;
        transform.position = position;
    }
    void LateUpdate()
    {
        Vector3 position = new Vector3();

        position = -mainCamera.transform.position * speed / 100 + mainCamera.transform.position + initialPosition;
        transform.position = position;
    }
}
