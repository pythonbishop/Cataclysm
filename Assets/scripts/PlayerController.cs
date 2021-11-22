using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public float verticalIn;
    public float horizontalIn;
    public Vector3 velocity;
    public float jetpackForce;
    public Vector3 acceleration;
    public float drag;
    public Vector3 dragForce;
    float[] centerScreen;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        centerScreen = new float[] {Screen.width/2, Screen.height/2};
        verticalIn = Input.GetAxis("Vertical");
        horizontalIn = Input.GetAxis("Horizontal");
        
        if (Input.mousePosition.x < centerScreen[0])
        {
            //player face left here
        }
        else
        {
            //player face right here
        }

        acceleration = (Vector3.right * horizontalIn + Vector3.up * verticalIn) * jetpackForce * Time.deltaTime;
        dragForce = -velocity * drag * Time.deltaTime;

        velocity += acceleration + dragForce;
        transform.Translate(velocity * Time.deltaTime);
    }
}
