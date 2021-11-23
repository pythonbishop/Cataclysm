using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    // Start is called before the first frame update
    public float angle = 0;
    public Vector3 offsetVector;
    public Vector3 bulletSpawn;
    SpriteRenderer spriteRenderer;
    Vector3 offsetRight;
    Vector3 offsetLeft;
    public float trim;
    public float gunLength;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sortingLayerName = "character";
        offsetLeft = new Vector3(-offsetVector.x, offsetVector.y, offsetVector.z);
        offsetRight = offsetVector;
    }
    // Update is called once per frame
    void Update()
    {
        
        if ((int)angle < 90 & (int)angle > -90)
        {
        angle = Mathf.Atan2(Input.mousePosition[1] - Screen.height/2 - offsetRight.y * trim, Input.mousePosition[0] - Screen.width/2 - offsetRight.x * trim) * Mathf.Rad2Deg;

        spriteRenderer.flipX = false;
        transform.localPosition = offsetRight;
        transform.SetPositionAndRotation(transform.position, Quaternion.AngleAxis(angle, Vector3.forward));
        bulletSpawn = transform.position + Vector3.Normalize(new Vector3(Input.mousePosition[0] - Screen.width/2 - offsetRight.x * trim, Input.mousePosition[1] - Screen.height/2 - offsetRight.y * trim)) * gunLength;
        }

        else if ((int)angle > 90 || (int)angle < -90)
        {
        angle = Mathf.Atan2(Input.mousePosition[1] - Screen.height/2 - offsetLeft.y * trim, Input.mousePosition[0] - Screen.width/2 - offsetLeft.x * trim) * Mathf.Rad2Deg;

        spriteRenderer.flipX = true;
        transform.localPosition = offsetLeft;
        transform.SetPositionAndRotation(transform.position, Quaternion.AngleAxis(angle-180, Vector3.forward));
        bulletSpawn = transform.position + Vector3.Normalize(new Vector3(Input.mousePosition[0] - Screen.width/2 - offsetLeft.x * trim, Input.mousePosition[1] - Screen.height/2 - offsetLeft.y * trim)) * gunLength;
        }

        angle = Mathf.Atan2(Input.mousePosition[1] - Screen.height/2, Input.mousePosition[0] - Screen.width/2) * Mathf.Rad2Deg;
    }
}
