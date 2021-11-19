using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public Sprite gunRightTexture;
    public Sprite gunLeftTexture;
    SpriteRenderer spriteRenderer;
    public float angle = 0;
    public Vector3 offsetVector;
    public Vector3 offsetLeft;
    public Vector3 offsetRight;
    public Vector3 bulletSpawn;
    public float trim;
    public bool facing; //True is right, False is left
    public float gunLength;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        offsetLeft = new Vector3(-offsetVector.x, offsetVector.y, offsetVector.z);
        offsetRight = offsetVector;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (angle < 90 & angle > -90)
        {
        angle = Mathf.Atan2(Input.mousePosition[1] - Screen.height/2 - offsetRight.y * trim, Input.mousePosition[0] - Screen.width/2 - offsetRight.x * trim) * Mathf.Rad2Deg;

        spriteRenderer.sprite = gunRightTexture;
        transform.localPosition = offsetRight;
        transform.SetPositionAndRotation(transform.position, Quaternion.AngleAxis(angle, Vector3.forward));
        facing = true;
        bulletSpawn = transform.position + Vector3.Normalize(new Vector3(Input.mousePosition[0] - Screen.width/2 - offsetRight.x * trim, Input.mousePosition[1] - Screen.height/2 - offsetRight.y * trim)) * gunLength;
        }

        else if (angle > 90 || angle < -90)
        {
        angle = Mathf.Atan2(Input.mousePosition[1] - Screen.height/2 - offsetLeft.y * trim, Input.mousePosition[0] - Screen.width/2 - offsetLeft.x * trim) * Mathf.Rad2Deg;

        spriteRenderer.sprite = gunLeftTexture;
        transform.localPosition = offsetLeft;
        transform.SetPositionAndRotation(transform.position, Quaternion.AngleAxis(angle-180, Vector3.forward));
        facing = false;
        bulletSpawn = transform.position + Vector3.Normalize(new Vector3(Input.mousePosition[0] - Screen.width/2 - offsetLeft.x * trim, Input.mousePosition[1] - Screen.height/2 - offsetLeft.y * trim)) * gunLength;
        }
    }
}
