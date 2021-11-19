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
    float[] centerScreen;
    void Start()
    {
        centerScreen = new float[] {Screen.width/2, Screen.height/2};
    }

    // Update is called once per frame
    void Update()
    {
        verticalIn = Input.GetAxis("Vertical");
        horizontalIn = Input.GetAxis("Horizontal");
        
        if (Input.mousePosition.x < centerScreen[0])
        {
            //player face left 
        }
        else
        {
            //player face right
        }

        acceleration = (Vector3.right * horizontalIn + Vector3.up * verticalIn)*jetpackForce/10;

        velocity += acceleration * Time.deltaTime;
        transform.Translate(velocity);
    }
}
