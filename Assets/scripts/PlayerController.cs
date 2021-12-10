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
    SpriteRenderer selfSprite;
    void Start()
    {
        selfSprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        verticalIn = Input.GetAxis("Vertical");
        horizontalIn = Input.GetAxis("Horizontal");

        acceleration = (Vector3.right * horizontalIn + Vector3.up * verticalIn) * jetpackForce * Time.deltaTime;
        dragForce = -velocity * drag * Time.deltaTime;

        velocity += acceleration + dragForce;
        transform.Translate(velocity * Time.deltaTime);
    }
}
