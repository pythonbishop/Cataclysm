using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    // Start is called before the first frame update
    public float angle = 0;
    public float trim;
    public float gunLength;
    public Vector3 bulletSpawn;
    public Vector3 offsetRight;
    public Vector3 offsetLeft;
    SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sortingLayerName = "character";
    }
    // Update is called once per frame
    void Update()
    {
        
        //gun face right
        if ((int)angle < 90 & (int)angle > -90)
        {
        //angle from gun to mouse
        angle = Mathf.Atan2(Input.mousePosition[1] - Screen.height/2 - offsetRight.y * trim, Input.mousePosition[0] - Screen.width/2 - offsetRight.x * trim) * Mathf.Rad2Deg;

        spriteRenderer.flipX = false;
        spriteRenderer.sortingOrder = 2;
        transform.localPosition = offsetRight;
        transform.SetPositionAndRotation(transform.position, Quaternion.AngleAxis(angle, Vector3.forward));
        bulletSpawn = transform.position + Vector3.Normalize(new Vector3(Input.mousePosition[0] - Screen.width/2 - offsetRight.x * trim, Input.mousePosition[1] - Screen.height/2 - offsetRight.y * trim)) * gunLength;
        }

        //gun face left
        else if ((int)angle > 90 || (int)angle < -90)
        {
        //angle from gun to mouse
        angle = Mathf.Atan2(Input.mousePosition[1] - Screen.height/2 - offsetLeft.y * trim, Input.mousePosition[0] - Screen.width/2 - offsetLeft.x * trim) * Mathf.Rad2Deg;

        spriteRenderer.flipX = true;
        spriteRenderer.sortingOrder = 0;
        transform.localPosition = offsetLeft;
        transform.SetPositionAndRotation(transform.position, Quaternion.AngleAxis(angle-180, Vector3.forward));
        bulletSpawn = transform.position + Vector3.Normalize(new Vector3(Input.mousePosition[0] - Screen.width/2 - offsetLeft.x * trim, Input.mousePosition[1] - Screen.height/2 - offsetLeft.y * trim)) * gunLength;
        }

        angle = Mathf.Atan2(Input.mousePosition[1] - Screen.height/2, Input.mousePosition[0] - Screen.width/2) * Mathf.Rad2Deg;
    }
}
