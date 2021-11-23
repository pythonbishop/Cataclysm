using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public float verticalIn;
    public float horizontalIn;
    public float jetpackForce;
    public float drag;
    public Vector3 velocity;
    Vector3 acceleration;
    Vector3 dragForce;
    float[] centerScreen;
    SpriteRenderer selfSprite;
    void Start()
    {
        centerScreen = new float[] {Screen.width/2, Screen.height/2};
        selfSprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        centerScreen[0] = Screen.width/2;
        centerScreen[1] = Screen.height/2;

        verticalIn = Input.GetAxis("Vertical");
        horizontalIn = Input.GetAxis("Horizontal");
        
        if (Input.mousePosition.x < centerScreen[0])
        {
            selfSprite.flipX = true;
        }
        else
        {
            selfSprite.flipX = false;
        }

        acceleration = (Vector3.right * horizontalIn + Vector3.up * verticalIn) * jetpackForce * Time.deltaTime;
        dragForce = -velocity * drag * Time.deltaTime;

        velocity += acceleration + dragForce;
        transform.Translate(velocity * Time.deltaTime);
    }
}
