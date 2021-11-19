using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    // Start is called before the first frame update
        public GameObject player;
        public GameObject bullet;
        public Vector3 posVector;
        public Sprite gunRightTexture;
        public Sprite gunLeftTexture;
        Vector3 mousePos;
        SpriteRenderer spriteRenderer;
        public float initialBuletDelay = 0.5f;
        public float angle = 0;
        public float playerOffsetX;
        public float playerOffsetY;
        float[] newPos = {0, 0};
        float currentDelay = 0.0f;
        bool mousePress;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Input.mousePosition;
        angle = Mathf.Atan2(mousePos[1] - Screen.height/2, mousePos[0] - Screen.width/2) * Mathf.Rad2Deg;
        
        if (angle < 90 & angle > -90)
        {
        newPos[0] = player.transform.position.x + playerOffsetX;
        spriteRenderer.sprite = gunRightTexture;
        transform.SetPositionAndRotation(posVector, Quaternion.AngleAxis(angle, Vector3.forward));
        }
        else if (angle > 90 || angle < -90)
        {
        newPos[0] = player.transform.position.x - playerOffsetX;
        spriteRenderer.sprite = gunLeftTexture;
        transform.SetPositionAndRotation(posVector, Quaternion.AngleAxis(angle-180, Vector3.forward));
        }

        newPos[1] = player.transform.position.y + playerOffsetY;
        posVector = new Vector3(newPos[0], newPos[1], 0);


        if (Input.GetMouseButtonDown(0))
        {
            mousePress = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            mousePress = false;
            currentDelay = 0.0f;
        }

        if (currentDelay <= 0 & mousePress)
        {
            currentDelay = initialBuletDelay;
            spawnBullet();
        }
        else if (mousePress)
        {
            currentDelay -= Time.deltaTime;
        }

        print("m1");
        
    }

    void spawnBullet()
    {
        GameObject obj = Instantiate(bullet, posVector, transform.rotation);
        obj.GetComponent<BulletMove>().direction = Vector3.Normalize(transform.position - player.transform.position);
    }
}
