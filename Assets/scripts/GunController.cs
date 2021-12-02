using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    // Start is called before the first frame update
    public float angle;
    public float gunLength;
    public Vector3 bulletSpawn;
    public Vector3 offsetRight;
    public Vector3 offsetLeft;
    public Vector3 gunToMouse;
    Camera camera;
    Vector3 mouseWorldPos;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        camera = Camera.main;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sortingLayerName = "character";
    }
    // Update is called once per frame
    void Update()
    {
        mouseWorldPos = camera.ScreenToWorldPoint(new Vector3(Input.mousePosition[0], Input.mousePosition[1]));
        gunToMouse = new Vector3(mouseWorldPos.x - transform.position[0], mouseWorldPos.y - transform.position[1]);
        angle = Vector3.Angle(Vector3.right, gunToMouse);
        
        bulletSpawn = transform.position + Vector3.Normalize(gunToMouse) * gunLength;
        transform.SetPositionAndRotation(transform.position, Quaternion.AngleAxis(angle, Vector3.forward));
        
        if ((int)angle < 90 & (int)angle > -90)
        {
        //gun face right
        spriteRenderer.flipX = false;
        spriteRenderer.sortingOrder = 2;
        transform.localPosition = offsetRight;
        }

        else if ((int)angle > 90 || (int)angle < -90)
        {
        //gun face left
        spriteRenderer.flipX = true;
        spriteRenderer.sortingOrder = 0;
        transform.localPosition = offsetLeft;
        }
    }
}
